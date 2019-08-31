FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["msac-tpa-new/msac-tpa-new.csproj", "msac-tpa-new/"]
RUN dotnet restore "msac-tpa-new/msac-tpa-new.csproj"
COPY . .
WORKDIR "/src/msac-tpa-new"
RUN dotnet build "msac-tpa-new.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "msac-tpa-new.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "msac-tpa-new.dll"]