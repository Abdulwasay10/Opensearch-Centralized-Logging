# OpenSearch Centralized Logging System with Serilog Integration

Production-ready centralized logging infrastructure using OpenSearch, Serilog, and Docker for real-time application monitoring and analytics.

## Overview

A modern, efficient logging infrastructure that demonstrates direct log shipping from .NET applications to OpenSearch using Serilog's OpenSearch sink. This approach eliminates the need for Logstash and intermediate log files, resulting in:

- No file-based logging overhead
- No merge conflicts from log files in Git
- Smaller Docker images
- Real-time log ingestion
- Simplified architecture

Perfect for DevOps engineers looking to implement clean, maintainable logging infrastructure. Specially for large and bulky logging like event logging.

## Key Features

- Direct Log Shipping: Logs flow directly from application to OpenSearch via HTTP
- Real-time Visualization: OpenSearch Dashboards for instant log monitoring and analysis
- Structured Logging: Rich event-driven logging with contextual metadata
- Minimal Container Stack: Only OpenSearch + Dashboards, no intermediate services
- High Performance: Batched transmission with configurable intervals
- Advanced Querying: Full-text search and filtering across all application logs
- Zero File Artifacts: No log files in repository or containers

## Flow Chart
<img width="222" height="451" alt="image" src="https://github.com/user-attachments/assets/549fd39e-3bb6-4910-8cdd-a99593994706" />



### Traditional Logstash Approach Issues:

- Log files accumulate in containers → Larger Docker images
- Files committed to Git → Merge conflicts on every push
- Extra layer in stack → More complexity, more points of failure
- File I/O overhead → Slower performance

### Direct Serilog Sink Advantages:

- Zero file artifacts - Logs go straight to database
- Cleaner Git history - No log file commits
- Smaller containers - No accumulated log files
- Real-time - Immediate visibility in dashboards
- Simpler stack - Fewer moving parts to maintain


## Quick Start
### Prerequisites

1. Docker & Docker Compose
2. .NET 8.0 SDK

#### Step 1: Start OpenSearch Infrastructure

Clone repository
```bash
git clone <repository-url>
cd <repository-name>
```

### Start OpenSearch and Dashboards

```bash
docker-compose up -d
```

#### Verify services are running

```bash
docker-compose ps
```
Expected output:
NAME          STATUS    PORTS
opensearch    Up        0.0.0.0:9201->9200/tcp
dashboards    Up        0.0.0.0:5602->5601/tcp

### Step 2: Run the .NET Demo Application

```bash
cd OsSerilogDemo/OsSerilogDemo
```

#### Restore dependencies

```bash
dotnet restore
```

#### Run the application

```bash
dotnet run
```
You'll see logs being generated and shipped directly to OpenSearch:
```bash
[11:30:00 INF] Demo app starting at 10/17/2025 11:30:00 AM
[11:30:00 INF] Order placed successfully.
[11:30:00 WRN] Payment latency higher than threshold.
[11:30:00 ERR] Something went wrong processing order 1234
```

### Step 3: View Logs in OpenSearch Dashboards

1. Open browser: http://localhost:5602/opensearch
2. Go to Management → Index Patterns
3. Create pattern: event-logs-*
4. Select @timestamp as time field
5. Navigate to Discover to see your logs


#### Index Strategy

Logs are automatically organized into daily indices:
```bash
event-logs-2025.10.17
event-logs-2025.10.18
event-logs-2025.10.19
...
```

## Benefits:

- Easy data retention management
- Better query performance
- Simplified cleanup of old logs

## Sample Dashboard:

<img width="1294" height="696" alt="image" src="https://github.com/user-attachments/assets/8488440c-2200-467c-8c7e-7d71f4ae37ce" />


## Useful Resources

- [OpenSearch Documentation](https://opensearch.org/docs/)
- [Serilog Wiki](https://github.com/serilog-contrib/serilog-sinks-opensearch)
- [Docker Compose Reference](https://docs.docker.com/compose/compose-file/)]
- [Structured Logging Best Practices](https://github.com/serilog/serilog/wiki/Structured-Data)
