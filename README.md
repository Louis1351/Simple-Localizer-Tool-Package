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
![Modification](https://github.com/Louis1351/Simple-Localizer-Tool-Package/blob/main/tutorials/Screenshot_8.png)

### Translation
![Translation](https://github.com/Louis1351/Simple-Localizer-Tool-Package/blob/main/tutorials/Screenshot_9.png)

### Settings
![Settings](https://github.com/Louis1351/Simple-Localizer-Tool-Package/blob/main/tutorials/Screenshot_10.png)

<a name="Components"/>

# Components
![Image Component](https://github.com/Louis1351/Simple-Localizer-Tool-Package/blob/main/tutorials/Screenshot_4.png)
![Text Component](https://github.com/Louis1351/Simple-Localizer-Tool-Package/blob/main/tutorials/Screenshot_5.png)
![Key Component](https://github.com/Louis1351/Simple-Localizer-Tool-Package/blob/main/tutorials/Screenshot_6.png)

![AutoLanguage Component](https://github.com/Louis1351/Simple-Localizer-Tool-Package/blob/main/tutorials/Screenshot_11.png)

<a name="InCode"/>

# In Code
```csharp
for (int i = 0 ; i < 10; i++)
{
// Code to execute.
}
```





