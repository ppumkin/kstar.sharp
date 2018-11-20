#!/bin/bash

docker build -f kstar.sharp.aspnetcore/Dockerfile . -t kstar.sharp/aspnetcore
docker build -f kstar.sharp.console/Dockerfile . -t kstar.sharp/console
