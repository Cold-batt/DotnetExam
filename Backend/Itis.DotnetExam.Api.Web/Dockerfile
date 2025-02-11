FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Itis.DotnetExam.Api.Web/Itis.DotnetExam.Api.Web.csproj", "Itis.DotnetExam.Api.Web/"]
COPY ["Itis.DotnetExam.Api.PostgreSql/Itis.DotnetExam.Api.PostgreSql.csproj", "Itis.DotnetExam.Api.PostgreSql/"]
COPY ["Itis.DotnetExam.Api.Core/Itis.DotnetExam.Api.Core.csproj", "Itis.DotnetExam.Api.Core/"]
COPY ["Itis.DotnetExam.Api.Contracts/Itis.DotnetExam.Api.Contracts.csproj", "Itis.DotnetExam.Api.Contracts/"]
COPY ["Itis.DotnetExam.Api.Mediator/Itis.DotnetExam.Api.Mediator.csproj", "Itis.DotnetExam.Api.Mediator/"]
COPY ["Itis.DotnetExam.Api.MongoDb/Itis.DotnetExam.Api.MongoDb.csproj", "Itis.DotnetExam.Api.MongoDb/"]
COPY ["Itis.DotnetExam.Api.SignalR/Itis.DotnetExam.Api.SignalR.csproj", "Itis.DotnetExam.Api.SignalR/"]
COPY ["Itis.DotnetExam.Api.RabbitMq/Itis.DotnetExam.Api.RabbitMq.csproj", "Itis.DotnetExam.Api.RabbitMq/"]
RUN dotnet restore "Itis.DotnetExam.Api.Web/Itis.DotnetExam.Api.Web.csproj"
COPY . .
WORKDIR "/src/Itis.DotnetExam.Api.Web"
RUN dotnet build "Itis.DotnetExam.Api.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Itis.DotnetExam.Api.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Itis.DotnetExam.Api.Web.dll"]
