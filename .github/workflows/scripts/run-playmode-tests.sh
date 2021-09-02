xvfb-run --auto-servernum --server-args='-screen 0 640x480x24' \
        /opt/Unity/Editor/Unity \
        -batchmode \
        -logFile "$GITHUB_WORKSPACE/playmode.log" \
        -projectPath "$GITHUB_WORKSPACE" \
        -runTests \
        -testPlatform PlayMode \
        -testResults "$GITHUB_WORKSPACE/playmode-results.xml" \
        -enableCodeCoverage \
        -coverageResultsPath "$GITHUB_WORKSPACE/CodeCoverage/" \
        -coverageOptions "assemblyFilters:-*unity*" \
        -burst-disable-compilation

# Catch exit code
EDIT_MODE_=$?

# Print unity log output
cat "$GITHUB_WORKSPACE/playmode.log"

cat /__w/UMVC/UMVC/playmode-results.xml

# Display results
if [ $EDIT_MODE_ -eq 0 ]; then
  echo "Run succeeded, no failures occurred";
elif [ $EDIT_MODE_ -eq 2 ]; then
  echo "Run succeeded, some tests failed";
elif [ $EDIT_MODE_ -eq 3 ]; then
  echo "Run failure (other failure)";
else
  echo "Unexpected exit code $EDIT_MODE_";
fi

exit $EDIT_MODE_