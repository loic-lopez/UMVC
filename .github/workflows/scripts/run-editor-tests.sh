xvfb-run --auto-servernum --server-args='-screen 0 640x480x24' \
        /opt/Unity/Editor/Unity \
        -batchmode \
        -logFile "$GITHUB_WORKSPACE/editmode.log" \
        -projectPath "$GITHUB_WORKSPACE" \
        -runTests \
        -testPlatform editmode \
        -testResults "$GITHUB_WORKSPACE/editmode-results.xml" \
        -enableCodeCoverage \
        -coverageResultsPath "$GITHUB_WORKSPACE/CodeCoverage/" \
        -burst-disable-compilation

# Catch exit code
EDIT_MODE_EXIT_CODE=$?

# Print unity log output
cat "$GITHUB_WORKSPACE/editmode.log"

# Display results
if [ $EDIT_MODE_EXIT_CODE -eq 0 ]; then
  echo "Run succeeded, no failures occurred";
elif [ $EDIT_MODE_EXIT_CODE -eq 2 ]; then
  echo "Run succeeded, some tests failed";
elif [ $EDIT_MODE_EXIT_CODE -eq 3 ]; then
  echo "Run failure (other failure)";
else
  echo "Unexpected exit code $EDIT_MODE_EXIT_CODE";
fi