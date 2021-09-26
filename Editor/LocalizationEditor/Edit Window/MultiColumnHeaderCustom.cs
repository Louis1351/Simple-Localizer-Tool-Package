using System.Collections.Generic;
using System.Linq;
using LS.Localiser.CSV;
using LS.Localiser.Utils;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace LS.Localiser.Editor
{
    [System.Serializable]
    public class MultiColumnHeaderCustom
    {
        private class Item
        {
            public string key = "";
            public SystemLanguage language;
            public string tags = "";
            public string textTags = "";
            public string spriteTags = "";
            public string clipTags = "";

            public Item(string key, string tags, string textTags, string spriteTags, string clipTags, SystemLanguage language)
            {
                this.key = key;
                this.tags = tags;
                this.textTags = textTags;
                this.spriteTags = spriteTags;
                this.clipTags = clipTags;
                this.language = language;
            }
        }
        private MultiColumnHeader columnHeader = null;

        [SerializeField]
        private TextLocalizerEditWindow window = null;
        private Dictionary<string, LocalizationSystem.LocalizationItem>[] dictionaries = null;
        private List<Item> items = null;
        private string key = "";
        private Vector2 scrollPos = Vector2.zero;
        private int currentPage = 0;
        public MultiColumnHeaderCustom(TextLocalizerEditWindow _window)
        {
            window = _window;
        }

        private void InitializeMultiColumn()
        {
            MultiColumnHeaderState.Column[] columns = new MultiColumnHeaderState.Column[]
            {
                new MultiColumnHeaderState.Column()
                {
                    headerContent = new GUIContent("Key"),
                    autoResize = true,
                    headerTextAlignment = TextAlignment.Center,
                    allowToggleVisibility = false
                },
                new MultiColumnHeaderState.Column()
                {
                    headerContent = new GUIContent("Languages"),
                    autoResize = true,
                    headerTextAlignment = TextAlignment.Center,
                    allowToggleVisibility = false
                },
                   new MultiColumnHeaderState.Column()
                {
                    headerContent = new GUIContent("Sprite"),
                    autoResize = true,
                    headerTextAlignment = TextAlignment.Center,
                    allowToggleVisibility = false
                },
                  new MultiColumnHeaderState.Column()
                {
                    headerContent = new GUIContent("Audio"),
                    autoResize = true,
                    headerTextAlignment = TextAlignment.Center,
                    allowToggleVisibility = false
                },
                  new MultiColumnHeaderState.Column()
                {
                    headerContent = new GUIContent("Modify"),
                    autoResize = true,
                    headerTextAlignment = TextAlignment.Center,
                    allowToggleVisibility = false
                },
            };
            columnHeader = new MultiColumnHeader(new MultiColumnHeaderState(columns));
            columnHeader.height = 25;
            columnHeader.ResizeToFit();
        }

        public void OnGUI()
        {
            if (dictionaries == null || items == null)
                RefreshDictionaries();

            if (columnHeader == null)
                InitializeMultiColumn();

            Vector2 startPos = new Vector2(0.0f, 40.0f);

            Rect rectSearchField = new Rect(startPos + new Vector2(5.0f, 20.0f), new Vector2(window.position.size.x - 20.0f, 30.0f));
            Rect rectSearchClose = new Rect(startPos + new Vector2(window.position.size.x - 12.5f, 20.0f), new Vector2(30.0f, 30.0f));

            GUI.SetNextControlName("key");
            key = GUI.TextField(rectSearchField, key, GUI.skin.FindStyle("ToolbarSeachTextField"));

            if (GUI.Button(rectSearchClose, "", GUI.skin.FindStyle("ToolbarSeachCancelButton")))
            {
                key = "";
                GUI.FocusControl(null);
            }

            if ((Event.current.keyCode == KeyCode.Return || Event.current.keyCode == KeyCode.KeypadEnter))
            {
                GUI.FocusControl(null);
                window.Repaint();
            }

            GUIContent guiCtSelect = new GUIContent("Select", "To select the key to change");
            GUIContent guiCtRemove = new GUIContent("Remove", "To remove the linked keys to the lanuage files");
            GUIContent guiCtNext = new GUIContent(">", "Next Page");
            GUIContent guiCtPrev = new GUIContent("<", "Previous Page");
            int nbColumn = 5;

            // draw the column headers
            var headerRect = window.position;
            headerRect.position = startPos - new Vector2(0.0f, 10.0f);
            headerRect.height = columnHeader.height;

            float xScroll = 0;
            columnHeader.OnGUI(headerRect, xScroll);

            // draw the column's contents
            float startScrollY = startPos.y + 40.0f;
            float scollH = 700 + 50;

            scrollPos = GUI.BeginScrollView(new Rect(0, startScrollY, window.position.width, window.position.height - startScrollY), scrollPos, new Rect(0, startScrollY, window.position.width - 100, scollH));


            for (int i = 0; i < nbColumn; i++)
            {
                // calculate column content rect
                var rectColumn = columnHeader.GetColumnRect(i);

                var contentRect = rectColumn;
                contentRect.x -= xScroll;
                contentRect.y = startPos.y + 5;
                contentRect.yMax = window.position.yMax;
                contentRect.width = contentRect.width * 0.5f;
                contentRect.height = 30;

                var LineRect = contentRect;

                LineRect.width = rectColumn.width;

                int nbKey = 0;
                for (int j = (key != "") ? 0 : (currentPage * 20);
                j < ((key != "") ? items.Count : (Mathf.Min(currentPage * 20 + 20, items.Count)));
                 ++j)
                {
                    if (key != "")
                    {
                        if (!items[j].key.Contains(key))
                            continue;

                        nbKey++;
                    }

                    contentRect.y += 35;
                    // custom content GUI...
                    switch (i)
                    {
                        case 0:
                            GUI.Label(contentRect, items[j].key);
                            break;
                        case 1:
                            GUI.Label(contentRect, items[j].textTags);
                            break;
                        case 2:
                            GUI.Label(contentRect, items[j].spriteTags);
                            break;
                        case 3:
                            GUI.Label(contentRect, items[j].clipTags);
                            break;
                        case 4:
                            if (GUI.Button(contentRect, guiCtSelect))
                            {
                                window.ModificationTab.ChangePopup((int)items[j].language);

                                window.TabEnum = TextLocalizerEditWindow.Tab.modification;
                                window.Key = items[j].key;
                            }
                            contentRect.x += contentRect.width;
                            if (GUI.Button(contentRect, guiCtRemove))
                            {
                                string[] tags = items[j].tags.Split(new string[] { " / " }, System.StringSplitOptions.None);
                                foreach (string tag in tags)
                                {
                                    LocalizationSystem.Remove(items[j].key, Translate.FindLanguageWithGoogleTag(tag));
                                }
                                RefreshDictionaries();
                            }
                            contentRect.x -= contentRect.width;
                            break;
                    }

                    LineRect.y = contentRect.y + 32f;
                    HorizontalLine(LineRect, Color.gray);
                    VerticalLine(LineRect, Color.gray);
                    if (nbKey > 19)
                    {
                        break;
                    }
                }
            }

            if (key == "")
            {
                Rect rectLabel = new Rect(new Vector2((window.position.size.x - 55.0f) * 0.5f, scollH + 40), new Vector2(30.0f, 30.0f));
                Rect rectBtnPrev = new Rect(new Vector2(10.0f, scollH + 40), new Vector2(30.0f, 30.0f));
                Rect rectBtnNext = new Rect(new Vector2(window.position.size.x - 55.0f, scollH + 40), new Vector2(30.0f, 30.0f));
                //  GUI.BeginGroup(rectBtns);
                int nbPage = (items.Count / 20);
                if (GUI.Button(rectBtnPrev, guiCtPrev))
                {
                    currentPage = Mathf.Max(0, currentPage - 1);
                }

                GUI.Label(rectLabel, currentPage + " / " + nbPage);

                if (GUI.Button(rectBtnNext, guiCtNext))
                {
                    currentPage = Mathf.Min(currentPage + 1, nbPage);
                }
            }
            //  GUI.EndGroup();
            GUI.EndScrollView();
        }
        private float GetscrollHeight()
        {
            float height = 0.0f;
            if (key != "")
            {
                for (int j = 0; j < items.Count; ++j)
                {

                    if (!items[j].key.Contains(key))
                        continue;
                    height += 35;
                }
            }
            else
            {
                height = 35 * items.Count;
            }

            return height;
        }
        private void VerticalLine(Rect _rect, Color color)
        {
            Rect rect = _rect;
            rect.y -= 32;
            GUIStyle verticalLine;
            verticalLine = new GUIStyle();
            verticalLine.normal.background = EditorGUIUtility.whiteTexture;
            verticalLine.fixedWidth = 1;

            var c = GUI.color;
            GUI.color = color;
            GUI.Box(rect, GUIContent.none, verticalLine);
            GUI.color = c;
        }

        private void HorizontalLine(Rect _rect, Color color)
        {
            GUIStyle horizontalLine;
            horizontalLine = new GUIStyle();
            horizontalLine.normal.background = EditorGUIUtility.whiteTexture;
            horizontalLine.margin = new RectOffset(0, 0, 4, 4);
            horizontalLine.fixedHeight = 1;

            var c = GUI.color;
            GUI.color = color;
            GUI.Box(_rect, GUIContent.none, horizontalLine);
            GUI.color = c;
        }
        public void RefreshDictionaries()
        {
            dictionaries = new Dictionary<string, LocalizationSystem.LocalizationItem>[window.LanguageTabNames.Length];
            items = new List<Item>();
            key = "";
            for (int i = 0; i < dictionaries.Length; ++i)
            {
                SystemLanguage l = (SystemLanguage)i;
                dictionaries[i] = LocalizationSystem.GetDictionary(l);
                if (dictionaries[i] != null)
                {
                    foreach (KeyValuePair<string, LocalizationSystem.LocalizationItem> elem in dictionaries[i])
                    {
                        string Gtag = Translate.FindGoogleTag(l);
                        Item findItem = ContainKey(elem.Key);
                        if (findItem != null)
                        {
                            findItem.tags += " / " + Gtag;

                            if (elem.Value.text != "")
                                findItem.textTags += " / " + Gtag;

                            if (elem.Value.spritePath != "")
                            {
                                findItem.spriteTags += " / " + Gtag;
                            }

                            if (elem.Value.clipPath != "")
                            {
                                findItem.clipTags += " / " + Gtag;
                            }
                        }
                        else
                        {
                            items.Add(new Item(elem.Key, Gtag, (elem.Value.text != "") ? Gtag : "", (elem.Value.spritePath != "") ? Gtag : "", (elem.Value.clipPath != "") ? Gtag : "", l));
                        }
                    }
                }
            }

            items = items.OrderBy((item) => item.key).ToList();
        }

        private Item ContainKey(string _key)
        {
            for (int i = 0; i < items.Count; ++i)
            {
                if (items[i].key == _key)
                {
                    return items[i];
                }
            }

            return null;
        }
    }
}