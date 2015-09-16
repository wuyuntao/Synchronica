$synchronicaOutputPath = "$PSScriptRoot\..\Synchronica\bin\Debug"
$unityAssemblyPath = "$PSScriptRoot\..\SynchronicaUnityExamples\Assets\Assemblies"

cp "$synchronicaOutputPath\*.dll" $unityAssemblyPath