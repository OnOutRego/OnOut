#!/usr/bin/env bash
set -euo pipefail
ROOT="$(cd "$(dirname "$0")" && pwd)"

# 1) bump target framework in all csproj files
find "$ROOT" -name '*.csproj' -print0 | xargs -0 sed -i 's/net8.0/net10.0/g'

# 2) remove Swashbuckle and ensure OpenApi + JwtBearer + Scalar packages
while IFS= read -r -d '' proj; do
  echo "Processing $proj"
  dotnet remove "$proj" package Swashbuckle.AspNetCore || true
  dotnet remove "$proj" package Swashbuckle || true

  # add/update required packages
  dotnet add "$proj" package Microsoft.AspNetCore.Authentication.JwtBearer --version 10.0.0 || true
  dotnet add "$proj" package Microsoft.AspNetCore.OpenApi --version 10.0.0 || true
  dotnet add "$proj" package Scalar.AspNetCore --version 2.14.0 || true
done < <(find "$ROOT" -name '*.csproj' -print0)

# 3) restore
dotnet restore "$ROOT"

echo "Upgrade completed. Edit Program.cs to remove Swashbuckle middleware and call the Scalar.AspNetCore APIs to enable the UI."
