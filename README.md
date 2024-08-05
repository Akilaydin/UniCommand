# Microservices Project: CommandsService and PlatformService

## Overview
This repository hosts a microservices-based application comprising two core services: CommandsService and PlatformService.

## Architecture
- **PlatformService**: Maintains a repository of platforms such as .NET, Ubuntu, etc.
- **CommandsService**: Stores, using in-memory database for simplicity, terminal commands for platforms interacts with the PlatformService to get an up-to-date list of platforms and keep it consistant.

## Technologies
- **PostgreSQL**: Provides persistent storage for the PlatformService, ensuring data durability.
- **RabbitMQ**: Facilitates asynchronous message passing between services, enhancing decoupling and reliability.
- **Docker**: Encapsulates service environments within containers to promote consistency across development, testing, and production phases.
- **Kubernetes**: Orchestrates the containerized services, managing their deployment, scaling, and load balancing effectively.
