# Simple-Localizer-tool
 A simple and easy edit tool for the Text/Sprite/Audio localization. (using google api to translate)<br>
 Can create mutiple CSV files for each language in the Resource folder.<br>
 The assets are currently load from an Asset Bundle.
 
# Table of contents
* [How to install](#HowTo)
* [CSV Edition](#CSV)
* [Components](#Components)
* [In Code](#InCode)

<a name="HowTo"/>

# How to install
Go to Window > Package Manager > Add package from URL... <br>
Add this url https://github.com/Louis1351/Simple-Localizer-Tool-Package.git

![Install Package](https://github.com/Louis1351/Simple-Localizer-Tool-Package/blob/main/tutorials/Screenshot_1.png)

### Create Asset Bundles
Before to add some CSV key with an asset in it, we have to generate the Streaming Assets and the Bundled Assets folder.<br>
Go to Localization > Build AssetBundles
 
![Assets Bundle Creation](https://github.com/Louis1351/Simple-Localizer-Tool-Package/blob/main/tutorials/Screenshot_2.png)

Under the  BundledAssets folder, we can find the localizerbundle folder.

![localizerbundle1](https://github.com/Louis1351/Simple-Localizer-Tool-Package/blob/main/tutorials/Screenshot_13.png)

Select the folder and add an asset bundle with the name localizerbundle.

![localizerbundle2](https://github.com/Louis1351/Simple-Localizer-Tool-Package/blob/main/tutorials/Screenshot_12.png)

Now we can add some asset into this folder.(Sprite Texture and AudioClip)<br>
<b>Warning!! Each time you add files, you have to rebuild the AssetBundles.</b>

### More Details

For more example, you can add the demo scene into your project.

![Demo](https://github.com/Louis1351/Simple-Localizer-Tool-Package/blob/main/tutorials/Screenshot_3.png)

<a name="CSV"/>

# CSV Edition
To open the edition window <br>
Go to Localization > Edit

![Assets Bundle Creation](https://github.com/Louis1351/Simple-Localizer-Tool-Package/blob/main/tutorials/Screenshot_2.png)

### Spreadsheet
In the spreadsheet, we can see all the CSV key for each language.<br>
If a tag's language shows up in one of this column, it means it 's already setup.<br>
For example here, for the flag key the sprite is set for French and English language.<br>
We can remove a key from all the CSV files or select a key to modify.

![Spreadsheet](https://github.com/Louis1351/Simple-Localizer-Tool-Package/blob/main/tutorials/Screenshot_7.png)

### Modification
Language -> Select the language to Edit.<br>
Search key -> Add new key or select a key already created.<br>
Sprite -> Select Sprite from the localizerbundle.<br>
Audio Clip -> Select Audio Clip from the localizerbundle.<br>
Text -> Add/Edit text<br>

Add/Replace/Remove the key fom the select language.

![Modification](https://github.com/Louis1351/Simple-Localizer-Tool-Package/blob/main/tutorials/Screenshot_8.png)

### Translation
Can translate one or multiple key from one language to another (Thanks to the Google API).

![Translation](https://github.com/Louis1351/Simple-Localizer-Tool-Package/blob/main/tutorials/Screenshot_9.png)

### Settings
Default Language -> Select the language to use in the editor. It also set this language as default for the game.<br>
File Format -> Only CSV Format for the moment.

![Settings](https://github.com/Louis1351/Simple-Localizer-Tool-Package/blob/main/tutorials/Screenshot_10.png)

<a name="Components"/>

# Components
ImageLocalizerUI to initialize SpriteRenderer and Image.

![Image Component](https://github.com/Louis1351/Simple-Localizer-Tool-Package/blob/main/tutorials/Screenshot_4.png)

TextLocalizer  to initialize Text and TextMesProUGUI.

![Text Component](https://github.com/Louis1351/Simple-Localizer-Tool-Package/blob/main/tutorials/Screenshot_5.png)

Find Key -> Assign the key to the component.

![Key Component](https://github.com/Louis1351/Simple-Localizer-Tool-Package/blob/main/tutorials/Screenshot_6.png)

Add AutoLanguage Component allows to change automatically the language to your current language's system.<br>

![AutoLanguage Component](https://github.com/Louis1351/Simple-Localizer-Tool-Package/blob/main/tutorials/Screenshot_11.png)


<a name="InCode"/>

# In Code
```csharp
using LS.Localiser.CSV;
public class NewBehaviourScript : MonoBehaviour
{
    void Start()
    {
        string key = "myKey";
        LocalizedString localizedStringStr = new LocalizedString(key);

        textComp.text = localizedStringStr.textValue;
        Sprite sprite = localizedStringStr.spriteValue;
        AudioClip audioClip = localizedStringStr.clipValue;
    }
}
```

# To Do





