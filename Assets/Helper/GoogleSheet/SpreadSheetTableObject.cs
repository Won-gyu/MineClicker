#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;

namespace Helper
{
    [CreateAssetMenu(fileName="New SpreadSheetTableObject", menuName="Helper/ScriptableObject/SpreadSheetTableObject")]
    public class SpreadSheetTableObject : ScriptableObject
    {
        [EnableIf("JobFinished")]
        [BoxGroup("GoogleSpreadSheet")]
        public string googleSpreadSheetId;

        [EnableIf("JobFinished")]
        [BoxGroup("GoogleSpreadSheet")]
        public UnityEngine.Object rootDirectory;

        [EnableIf("JobFinished")]
        [BoxGroup("GoogleSpreadSheet")]
        public SpreadSheetTableObjectParser parser;

        [Button]
        [EnableIf("JobFinished")]
        [BoxGroup("GoogleSpreadSheet")]
        private void Open()
        {
            Application.OpenURL("https://docs.google.com/spreadsheets/d/" + googleSpreadSheetId);
        }

        [Button]
        [EnableIf("JobFinished")]
        [BoxGroup("GoogleSpreadSheet")]
        public void Pull()
        {
            importedList = new List<KeyValuePair<string, string>>();
            progress = spreadSheets.Count;
            foreach(var spreadSheetTab in spreadSheets)
            {
                spreadSheetTab.parent = this;
                spreadSheetTab.Pull();
            }
        }

        [EnableIf("JobFinished")]
        [BoxGroup("GoogleSpreadSheet")]
        [ReadOnly]
        public int progress;
        public bool JobFinished
        {
            get { return progress <= 0; }
        }

        [Serializable]
        public class SpreadSheetObject
        {
            [GUIColor(0,1,1,1)]
            public string tableName = "Default";

            /*
                getTable:
                "TabName":
                [
                    { 
                        "첫번째 column, 첫번째 row": "첫번째 column, 현재 row",
                        "두번째 column, 첫번째 row": "두번째 column, 현재 row",
                        "세번째 column, 첫번째 row": "세번째 column, 현재 row",
                        "네번째 column, 첫번째 row": "네번째 column, 현재 row"
                    },
                    ...
                }

                getTableRows:
                "TabName":
                {
                    "첫번째 column, 첫번째 row": [1,2,3,4,5],
                    "첫번째 column, 두번째 row": ["a","b","c","d","e"],
                    "첫번째 column, 세번째 row": [1.4, 2.32, 3.1, 4.0],
                    ...
                }

                getTableAllRows:
                "TabName":
                [
                    [1,2,3,4,5,6,7,8],
                    [1,2,3,4,5,6,7,8],
                    [1,2,3,4,5,6,7,8],
                    ...
                ]
            */

            public GoogleSheet.QueryType queryType = GoogleSheet.QueryType.getTable;
            
            [HideInInspector]
            public SpreadSheetTableObject parent;

            public void Pull()
            {
                try 
                {
                    googleSheet = new GoogleSheet();
                    Debug.Log("@@@ TEST1");
                    googleSheet.webServiceUrl = HelperSettings.Instance.googleWebServiceUrl;
                    Debug.Log("@@@ TEST2");
                    googleSheet.servicePassword = HelperSettings.Instance.googleWebServicePassword;
                    Debug.Log("@@@ TEST3");
                    googleSheet.spreadsheetId = parent.googleSpreadSheetId;
                    Debug.Log("@@@ TEST4");
                    googleSheet.rawResponseCallback.AddListener(HandleErrors);
                    Debug.Log("@@@ TEST5");
                    googleSheet.processedResponseCallback.AddListener(Imported);
                    Debug.Log("@@@ TEST6");
                    switch(queryType)
                    {
                    case GoogleSheet.QueryType.getTable:
                        googleSheet.GetTable(tableName);
                        break;
                    case GoogleSheet.QueryType.getTableRows:
                        googleSheet.GetTableRows(tableName);
                        break;
                    case GoogleSheet.QueryType.getTableAllRows:
                        googleSheet.GetTableAllRows(tableName);
                        break;
                    }
                } catch (Exception e)
                {
                    Debug.LogError("[SpreadSheetTableObject] Spreadsheet errors - " + e.Data.Count);
                    --parent.progress;
                }
            }

            private void Imported(GoogleSheet.QueryType query, List<string> objTypeNames, List<string> jsonData)
            {
                try
                {
                    parent.Parse(query, objTypeNames, jsonData);
                } catch (Exception e)
                {
                    Debug.LogError("[SpreadSheetTableObject(Exception)] Parsing error!");
                    --parent.progress;
                }
            }

            private void HandleErrors(string msg)
            {
                Debug.Log("HandleErrors");
                --parent.progress;
            }

            private GoogleSheet googleSheet;
        }

        [EnableIf("JobFinished")]
        [BoxGroup("GoogleSpreadSheet")]
        [TableList(AlwaysExpanded=true, ShowIndexLabels=true, ShowPaging=true, NumberOfItemsPerPage=5)]
        public List<SpreadSheetObject> spreadSheets;

        public List<KeyValuePair<string, string>> importedList;

        protected virtual void Parse(GoogleSheet.QueryType query, List<string> objTypeNames, List<string> jsonData)
        {
            --progress;

            var path = AssetDatabase.GetAssetPath(rootDirectory);

            for (int i = 0; i < objTypeNames.Count; ++i)
            {
                var objName = objTypeNames[i];
                var json = jsonData[i];

                importedList.Add(new KeyValuePair<string, string>(objName, json));
            }

            if (progress <= 0)
            {
                var json = "{";
                for (int i = 0; i < importedList.Count; ++i)
                {
                    var keyValue = importedList[i];

                    var formatedText = "\""+keyValue.Key+"\":"+keyValue.Value;
                    json += formatedText;

                    if (i < importedList.Count-1)
                        json += ",";
                }
                json += "\n}";

                if (parser != null)
                {
                    json = parser.Parse(json);
                }

                var filePath = string.Format("{0}/{1}.json", path, name);
                if (FileUtils.ExistFile(filePath))
                {
                    FileUtils.DeleteFile(filePath);
                }
                FileUtils.Write(filePath, json, true);

                AssetDatabase.Refresh();
            }
            
        }
    }
}

#endif