FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["RK.Web/RK.Web.csproj", "RK.Web/"]
COPY ["RK.Repository/RK.Repository.csproj", "RK.Repository/"]
COPY ["RK.Model/RK.Model.csproj", "RK.Model/"]
COPY ["RK.Framework/RK.Framework.csproj", "RK.Framework/"]
COPY ["RK.Infrastructure/RK.Infrastructure.csproj", "RK.Infrastructure/"]
COPY ["RK.Service/RK.Service.csproj", "RK.Service/"]
RUN dotnet restore "RK.Web/RK.Web.csproj"
COPY . .
WORKDIR "/src/RK.Web"
RUN dotnet build "RK.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "RK.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "RK.Web.dll"]