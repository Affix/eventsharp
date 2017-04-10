FROM microsoft/dotnet:1.1.1-sdk
MAINTAINER Keiran Smith <opensource@keiran.scot>

COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o out
ENTRYPOINT ["dotnet", "out/events.dll"]
