#!/bin/bash

if ! type dotnet-sonarscanner &> /dev/null;
then
    dotnet tool install --global dotnet-sonarscanner
fi

if ! type reportgenerator &> /dev/null;
then
    dotnet tool install -g dotnet-reportgenerator-globaltool  
fi

if ! type dotnet-ef &> /dev/null;
then
    dotnet tool install --global dotnet-ef --version 5.0.17
fi