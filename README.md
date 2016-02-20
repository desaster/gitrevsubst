# [Gitrevsubst](http://github.com/desaster/gitrevsubst)

Gitrevsubst is a very simple program to add git revision to a file. The idea is to use it during build time as a replacement for [SubWCRev](https://tortoisesvn.net/docs/release/TortoiseSVN_en/tsvn-subwcrev.html).

## Example of use with Visual Studio

* Add gitrevsubst.exe to your PATH
* In your C# project, rename `Properties\AssemblyInfo.cs` to `AssemblyInfo.cs.tmpl`
* Add something like this to your .tmpl file:
```
[assembly: AssemblyInformationalVersion("1.0.0.$GITDATE$-$GITREV$")]
```
* Add the following command to the pre-build script of your project:
```
"gitrevsubst.exe" "$(SolutionDir).git" "$(ProjectDir)Properties\AssemblyInfo.cs.tmpl" "$(ProjectDir)Properties\AssemblyInfo.cs"
```
* Do not add AssemblyInfo.cs to version control!
* Example result:
```
[assembly: AssemblyInformationalVersion("1.0.0.20162002-44ce0c5")]
```
