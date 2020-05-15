#!/usr/bin/env python

import sys, os
import requests


# Set up your repository here
OWNER="loic-lopez"
REPO="UMVC"
WORKFLOW_NAME="UMVC.Core"
ARTIFACT_NAME="UMVC.Core.Build"

API_URL="https://api.github.com"


def get_workflow_id(workflow_name):
  response = requests.get("%s/repos/%s/%s/actions/workflows" % (API_URL, OWNER, REPO))
  print(response.status_code)
  json = response.json()
  for workflow in json['workflows']:
    if workflow['name'] == workflow_name:
      print(workflow['name'])
      return workflow['id']
  return None #FIXME: Exception

def get_latest_workflow_run_id(workflow_id):
  response = requests.get("%s/repos/%s/%s/actions/workflows/%s/runs" % (API_URL, OWNER, REPO, workflow_id))
  print(response.status_code)
  json = response.json()
  for workflow_run in json['workflow_runs']:

    # Only consider completed runs
    if workflow_run['status'] != "completed":
      continue
    if workflow_run['conclusion'] != "success":
      continue

    return workflow_run['id']

  return None #FIXME: Exception

def get_artifact_id(workflow_run_id, name):
  response = requests.get("%s/repos/%s/%s/actions/runs/%s/artifacts" % (API_URL, OWNER, REPO, workflow_run_id))
  print(response.status_code)
  json = response.json()
  for artifact in json['artifacts']:

    # Match by name
    if artifact['name'] == name:
      return artifact['id']

  return None #FIXME: Exception

def get_latest_artifact_url(workflow_name, artifact_name):

  workflow_id = get_workflow_id(workflow_name)
  print("found workflow %d" % workflow_id)

  workflow_run_id = get_latest_workflow_run_id(workflow_id)
  print("found workflow_run_id %d" % workflow_run_id)

  artifact_id = get_artifact_id(workflow_run_id, artifact_name)
  print("found artifact_id %d" % artifact_id)

  return "%s/repos/%s/%s/actions/artifacts/%s/zip" % (API_URL, OWNER, REPO, artifact_id)


if __name__ == "__main__":

  # Code to test the URL fetcher
  artifact_url = get_latest_artifact_url(WORKFLOW_NAME, ARTIFACT_NAME)
  print(artifact_url)
  
  zip = requests.get(artifact_url)

  pathname = os.path.dirname(sys.argv[0])
  output_directory = os.path.join(os.path.abspath(pathname), "../../../")

  file_path = os.path.join(output_directory, 'UMVC.Core.Build.zip')

  # Write the file
  with open(file_path, 'wb') as output:
    output.write(zip.content)

  print("Writed UMVC.Core.Build.zip to %s" % file_path)
  