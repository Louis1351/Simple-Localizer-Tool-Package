// Credit goes to Grimmdev https://gist.github.com/grimmdev/979877fcdc943267e44c

// We need this for parsing the JSON, unless you use an alternative.
// You will need SimpleJSON if you don't use alternatives.
// It can be gotten hither. http://wiki.unity3d.com/index.php/SimpleJSON
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using LS.Localiser.Utils.SimpleJSON;

namespace LS.Localiser.Utils
{
    public class Translate
    {
        public static SystemLanguage FindLanguageWithGoogleTag(string tag)
        {
            SystemLanguage language = SystemLanguage.Afrikaans;
            switch (tag)
            {
                case "af":
                    language = SystemLanguage.Afrikaans;
                    break;
                //
                // Résumé :
                //     Arabic.
                case "ar":
                    language = SystemLanguage.Arabic;
                    break;
                //
                // Résumé :
                //     Basque.
                case "eu":
                    language = SystemLanguage.Basque;
                    break;
                //
                // Résumé :
                //     Belarusian.
                case "be":
                    language = SystemLanguage.Belarusian;
                    break;
                //
                // Résumé :
                //     Bulgarian.
                case "bg":
                    language = SystemLanguage.Bulgarian;
                    break;
                //
                // Résumé :
                //     Catalan.
                case "ca":
                    language = SystemLanguage.Catalan;
                    break;
                //
                // Résumé :
                //     Czech.
                case "cs":
                    language = SystemLanguage.Czech;
                    break;
                //
                // Résumé :
                //     Danish.
                case "da":
                    language = SystemLanguage.Danish;
                    break;
                //
                // Résumé :
                //     Dutch.
                case "nl":
                    language = SystemLanguage.Dutch;
                    break;
                //
                // Résumé :
                //     English.
                case "en":
                    language = SystemLanguage.English;
                    break;
                //
                // Résumé :
                //     Estonian.
                case "et":
                    language = SystemLanguage.Estonian;
                    break;
                //
                // Résumé :
                //     Faroese.
                case "notfound":
                    //not found
                    break;
                //
                // Résumé :
                //     Finnish.
                case "fi":
                    language = SystemLanguage.Finnish;
                    break;
                //
                // Résumé :
                //     French.
                case "fr":
                    language = SystemLanguage.French;
                    break;
                //
                // Résumé :
                //     German.
                case "de":
                    language = SystemLanguage.German;
                    break;
                //
                // Résumé :
                //     Greek.
                case "el":
                    language = SystemLanguage.Greek;
                    break;
                //
                // Résumé :
                //     Hebrew.
                case "he":
                    language = SystemLanguage.Hebrew;
                    break;
                //
                // Résumé :
                //     Hungarian.
                case "hu":
                    language = SystemLanguage.Hungarian;
                    break;
                //
                // Résumé :
                //     Icelandic.
                case "is":
                    language = SystemLanguage.Icelandic;
                    break;
                //
                // Résumé :
                //     Indonesian.
                case "id":
                    language = SystemLanguage.Indonesian;
                    break;
                //
                // Résumé :
                //     Italian.
                case "it":
                    language = SystemLanguage.Italian;
                    break;
                //
                // Résumé :
                //     Japanese.
                case "ja":
                    language = SystemLanguage.Japanese;
                    break;
                //
                // Résumé :
                //     Korean.
                case "ko":
                    language = SystemLanguage.Korean;
                    break;
                //
                // Résumé :
                //     Latvian.
                case "lv":
                    language = SystemLanguage.Latvian;
                    break;
                //
                // Résumé :
                //     Lithuanian.
                case "lt":
                    language = SystemLanguage.Lithuanian;
                    break;
                //
                // Résumé :
                //     Norwegian.
                case "no":
                    language = SystemLanguage.Norwegian;
                    break;
                //
                // Résumé :
                //     Polish.
                case "pl":
                    language = SystemLanguage.Polish;
                    break;
                //
                // Résumé :
                //     Portuguese.
                case "pt":
                    language = SystemLanguage.Portuguese;
                    break;
                //
                // Résumé :
                //     Romanian.
                case "ro":
                    language = SystemLanguage.Romanian;
                    break;
                //
                // Résumé :
                //     Russian.
                case "ru":
                    language = SystemLanguage.Russian;
                    break;
                //
                // Résumé :
                //     Serbo-Croatian.
                case "sr":
                    language = SystemLanguage.SerboCroatian;
                    break;
                //
                // Résumé :
                //     Slovak.
                case "sk":
                    language = SystemLanguage.Slovak;
                    break;
                //
                // Résumé :
                //     Slovenian.
                case "sl":
                    language = SystemLanguage.Slovenian;
                    break;
                //
                // Résumé :
                //     Spanish.
                case "es":
                    language = SystemLanguage.Spanish;
                    break;
                //
                // Résumé :
                //     Swedish.
                case "sv":
                    language = SystemLanguage.Swedish;
                    break;
                //
                // Résumé :
                //     Thai.
                case "th":
                    language = SystemLanguage.Thai;
                    break;
                //
                // Résumé :
                //     Turkish.
                case "tr":
                    language = SystemLanguage.Turkish;
                    break;
                //
                // Résumé :
                //     Ukrainian.
                case "uk":
                    language = SystemLanguage.Ukrainian;
                    break;
                //
                // Résumé :
                //     Vietnamese.
                case "vi":
                    language = SystemLanguage.Vietnamese;
                    break;
                //
                // Résumé :
                //     ChineseSimplified.
                case "zh-CN":
                    language = SystemLanguage.ChineseSimplified;
                    break;
                //
                // Résumé :
                //     ChineseTraditional.
                case "zh-TW":
                    language = SystemLanguage.ChineseTraditional;
                    break;
            }
            return language;
        }

        public static string FindGoogleTag(SystemLanguage language)
        {
            string tag = "";
            switch (language)
            {
                case SystemLanguage.Afrikaans:
                    tag = "af";
                    break;
                //
                // Résumé :
                //     Arabic.
                case SystemLanguage.Arabic:
                    tag = "ar";
                    break;
                //
                // Résumé :
                //     Basque.
                case SystemLanguage.Basque:
                    tag = "eu";
                    break;
                //
                // Résumé :
                //     Belarusian.
                case SystemLanguage.Belarusian:
                    tag = "be";
                    break;
                //
                // Résumé :
                //     Bulgarian.
                case SystemLanguage.Bulgarian:
                    tag = "bg";
                    break;
                //
                // Résumé :
                //     Catalan.
                case SystemLanguage.Catalan:
                    tag = "ca";
                    break;
                //
                // Résumé :
                //     Chinese.
                case SystemLanguage.Chinese:
                    tag = "zh-CN";
                    break;
                //
                // Résumé :
                //     Czech.
                case SystemLanguage.Czech:
                    tag = "cs";
                    break;
                //
                // Résumé :
                //     Danish.
                case SystemLanguage.Danish:
                    tag = "da";
                    break;
                //
                // Résumé :
                //     Dutch.
                case SystemLanguage.Dutch:
                    tag = "nl";
                    break;
                //
                // Résumé :
                //     English.
                case SystemLanguage.English:
                    tag = "en";
                    break;
                //
                // Résumé :
                //     Estonian.
                case SystemLanguage.Estonian:
                    tag = "et";
                    break;
                //
                // Résumé :
                //     Faroese.
                case SystemLanguage.Faroese:
                    //not found
                    break;
                //
                // Résumé :
                //     Finnish.
                case SystemLanguage.Finnish:
                    tag = "fi";
                    break;
                //
                // Résumé :
                //     French.
                case SystemLanguage.French:
                    tag = "fr";
                    break;
                //
                // Résumé :
                //     German.
                case SystemLanguage.German:
                    tag = "de";
                    break;
                //
                // Résumé :
                //     Greek.
                case SystemLanguage.Greek:
                    tag = "el";
                    break;

                //
                // Résumé :
                //     Hebrew.
                case SystemLanguage.Hebrew:
                    tag = "he";
                    break;

                //
                // Résumé :
                //     Hungarian.
                case SystemLanguage.Hungarian:
                    tag = "hu";
                    break;
                //
                // Résumé :
                //     Icelandic.
                case SystemLanguage.Icelandic:
                    tag = "is";
                    break;
                //
                // Résumé :
                //     Indonesian.
                case SystemLanguage.Indonesian:
                    tag = "id";
                    break;
                //
                // Résumé :
                //     Italian.
                case SystemLanguage.Italian:
                    tag = "it";
                    break;
                //
                // Résumé :
                //     Japanese.
                case SystemLanguage.Japanese:
                    tag = "ja";
                    break;
                //
                // Résumé :
                //     Korean.
                case SystemLanguage.Korean:
                    tag = "ko";
                    break;
                //
                // Résumé :
                //     Latvian.
                case SystemLanguage.Latvian:
                    tag = "lv";
                    break;
                //
                // Résumé :
                //     Lithuanian.
                case SystemLanguage.Lithuanian:
                    tag = "lt";
                    break;
                //
                // Résumé :
                //     Norwegian.
                case SystemLanguage.Norwegian:
                    tag = "no";
                    break;
                //
                // Résumé :
                //     Polish.
                case SystemLanguage.Polish:
                    tag = "pl";
                    break;
                //
                // Résumé :
                //     Portuguese.
                case SystemLanguage.Portuguese:
                    tag = "pt";
                    break;
                //
                // Résumé :
                //     Romanian.
                case SystemLanguage.Romanian:
                    tag = "ro";
                    break;
                //
                // Résumé :
                //     Russian.
                case SystemLanguage.Russian:
                    tag = "ru";
                    break;
                //
                // Résumé :
                //     Serbo-Croatian.
                case SystemLanguage.SerboCroatian:
                    tag = "sr";
                    break;
                //
                // Résumé :
                //     Slovak.
                case SystemLanguage.Slovak:
                    tag = "sk";
                    break;
                //
                // Résumé :
                //     Slovenian.
                case SystemLanguage.Slovenian:
                    tag = "sl";
                    break;
                //
                // Résumé :
                //     Spanish.
                case SystemLanguage.Spanish:
                    tag = "es";
                    break;
                //
                // Résumé :
                //     Swedish.
                case SystemLanguage.Swedish:
                    tag = "sv";
                    break;
                //
                // Résumé :
                //     Thai.
                case SystemLanguage.Thai:
                    tag = "th";
                    break;
                //
                // Résumé :
                //     Turkish.
                case SystemLanguage.Turkish:
                    tag = "tr";
                    break;
                //
                // Résumé :
                //     Ukrainian.
                case SystemLanguage.Ukrainian:
                    tag = "uk";
                    break;
                //
                // Résumé :
                //     Vietnamese.
                case SystemLanguage.Vietnamese:
                    tag = "vi";
                    break;
                //
                // Résumé :
                //     ChineseSimplified.
                case SystemLanguage.ChineseSimplified:
                    tag = "zh-CN";
                    break;
                //
                // Résumé :
                //     ChineseTraditional.
                case SystemLanguage.ChineseTraditional:
                    tag = "zh-TW";
                    break;
            }
            return tag;
        }
        // We have use googles own api built into google Translator.
        public static IEnumerator Process(SystemLanguage source, SystemLanguage target, string sourceText, System.Action<string> result)
        {
            string sourceLanguage = FindGoogleTag(source);
            string targetLanguage = FindGoogleTag(target);

            if (targetLanguage == "" || sourceLanguage == "")
            {
                yield return null;
            }
            else
                yield return Process(sourceLanguage, targetLanguage, sourceText, result);
        }

        // Exactly the same as above but allow the user to change from Auto, for when google get's all Jerk Butt-y
        public static IEnumerator Process(string sourceLang, string targetLang, string sourceText, System.Action<string> result)
        {
            string url = "https://translate.googleapis.com/translate_a/single?client=gtx&sl="
                + sourceLang + "&tl=" + targetLang + "&dt=t&q=" + UnityWebRequest.EscapeURL(sourceText);

            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                if (webRequest.isDone)
                {
                    // Check to see if we don't have any errors.
                    if (string.IsNullOrEmpty(webRequest.error))
                    {
                        // Parse the response using JSON.
                        var N = JSONNode.Parse(webRequest.downloadHandler.text);
                        // Dig through and take apart the text to get to the good stuff.
                        string translatedText = "";
                        for (int i = 0; i < N[0].Count; ++i)
                            translatedText += N[0][i][0];
                        // This is purely for debugging in the Editor to see if it's the word you wanted.
                        result(translatedText);
                    }
                }
            }
            yield return null;
        }
    }
}