ls

unzip -o UMVC.Core.Build.zip -d UMVC.Core.Build

moveDllToDir() {
  mv "$1" "$2"
  echo "Moving dependency $1 to $2"
}


# player deps
moveDllToDir "UMVC.Core.Exceptions.dll" "Assets/UMVC/PlayerDependencies"
moveDllToDir "UMVC.Core.MVC.dll" "Assets/UMVC/PlayerDependencies"

# Editor deps
moveDllToDir "UMVC.Core.Components.dll" "Assets/UMVC/Editor/EditorDependencies"
moveDllToDir "UMVC.Core.Generation.dll" "Assets/UMVC/Editor/EditorDependencies"
moveDllToDir "UMVC.Core.Templates.dll" "Assets/UMVC/Editor/EditorDependencies"