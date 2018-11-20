#!/bin/bash

docker build -f kstar.sharp.aspnetcore/Dockerfile . -t kstar.sharp/aspnetcore
docker build -f kstar.sharp.api/Dockerfile . -t kstar.sharp/api