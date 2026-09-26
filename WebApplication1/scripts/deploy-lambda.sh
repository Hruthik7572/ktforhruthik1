#!/usr/bin/env bash
set -Eeuo pipefail

required_vars=(AWS_REGION AWS_STACK_NAME AWS_DEPLOYMENT_BUCKET AWS_DB_SECRET_ARN)
for name in "${required_vars[@]}"; do
  if [[ -z "${!name:-}" ]]; then
    printf 'Required environment variable %s is not set.\n' "$name" >&2
    exit 2
  fi
done

command -v dotnet >/dev/null 2>&1 || { echo 'dotnet is required on the Jenkins agent.' >&2; exit 127; }
command -v aws >/dev/null 2>&1 || { echo 'AWS CLI is required on the Jenkins agent.' >&2; exit 127; }

# Fail early if the Jenkins agent has no usable AWS identity.
aws sts get-caller-identity --region "$AWS_REGION" --output text >/dev/null

# Restore the repository-pinned AWS Lambda CLI and deploy the SAM stack.
dotnet tool restore
dotnet lambda deploy-serverless "$AWS_STACK_NAME" \
  --region "$AWS_REGION" \
  --s3-bucket "$AWS_DEPLOYMENT_BUCKET" \
  --template-parameters "DatabaseSecretArn=$AWS_DB_SECRET_ARN" \
  --disable-interactive
