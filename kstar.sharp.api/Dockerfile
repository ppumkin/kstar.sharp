#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 53106
EXPOSE 44380

FROM microsoft/dotnet:2.1-sdk-nanoserver-1803 AS build
WORKDIR /src
COPY ["kstar.sharp.api/kstar.sharp.api.csproj", "kstar.sharp.api/"]
RUN dotnet restore "kstar.sharp.api/kstar.sharp.api.csproj"
COPY . .
WORKDIR "/src/kstar.sharp.api"
RUN dotnet build "kstar.sharp.api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "kstar.sharp.api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "kstar.sharp.api.dll"]