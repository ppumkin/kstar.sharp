#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat



FROM microsoft/dotnet:2.1-sdk AS build-env
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# build runtime image
FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "kstar.sharp.aspnetcore.dll"]


#FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1803 AS base
#WORKDIR /app
#EXPOSE 62536
#EXPOSE 44311
#
#FROM microsoft/dotnet:2.1-sdk-nanoserver-1803 AS build
#WORKDIR /src
#COPY ["kstar.sharp.aspnetcore/kstar.sharp.aspnetcore.csproj", "kstar.sharp.aspnetcore/"]
#RUN dotnet restore "kstar.sharp.aspnetcore/kstar.sharp.aspnetcore.csproj"
#COPY . .
#WORKDIR "/src/kstar.sharp.aspnetcore"
#RUN dotnet build "kstar.sharp.aspnetcore.csproj" -c Release -o /app
#
#FROM build AS publish
#RUN dotnet publish "kstar.sharp.aspnetcore.csproj" -c Release -o /app
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app .
#ENTRYPOINT ["dotnet", "kstar.sharp.aspnetcore.dll"]