# npivalidator

## Introduction

This is a silly little console application where I had my first go with .NET Core in VS Code. It is not an education in code style, good practice or anything else. Judge not lest ye be punched in the face.

The purpose of the project is to validate National Provider Identifiers (NPIs). The input is one line of comma-separated NPIs in the npis.csv file and these are bumped up against http://www.hipaaspace.com one at a time with output written to the console. The reason I chose http://www.hipaaspace.com over https://npiregistry.cms.hhs.gov/ will remain a mystery to you (and me) until the end of time.

The API token is securely stored in `appsettings.json.pgp` using Blackbox. This file is useless to everyone except me.

## Running the Application

To run the application yourself you'll need to:

1. Clone the repository
2. Open the folder in VS Code (ensuring that your editor is setup for .NET Core development)
3. Create your own `appsettings.json` file that looks like (get a token from http://www.hipaaspace.com):
```json
{
    "access_token": "your-api-token-here"
}
```
4. From the root of the project execute `dotnet run` from a Terminal

