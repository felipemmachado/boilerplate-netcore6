#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
EXPOSE 80
EXPOSE 443
WORKDIR /
COPY ["/WebAPI/WebAPI.csproj", "/WebAPI/"]
COPY ["/Domain/Domain.csproj", "/Domain/"]
COPY ["/Application/Application.csproj", "/Application/"]
COPY ["/Infrastructure/Infrastructure.csproj", "/Infrastructure/"]
RUN dotnet restore "/WebAPI/WebAPI.csproj"
COPY . .
WORKDIR "/WebAPI"
RUN dotnet build "WebAPI.csproj" -c Development -o /app/build
RUN dotnet publish "WebAPI.csproj" -c Development -o /app/publish
WORKDIR /app
RUN cp -R /app/publish/* .
RUN dotnet tool install --global dotnet-ef
ENV PATH "$PATH:/root/.dotnet/tools"
ENTRYPOINT ["dotnet", "WebAPI.dll"]