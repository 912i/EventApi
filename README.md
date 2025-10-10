# Event API - Gestion d'Événements avec Notifications Temps Réel

## Description
Cette API backend est une implémentation légère mais robuste d'un système de gestion d'événements. Elle permet de créer, lire et diffuser des événements en temps réel, avec des fonctionnalités de stockage, de caching, et de traitement asynchrone. Ce projet démontre des compétences avancées en architecture logicielle (Clean Architecture, DDD, CQRS) et l'intégration de technologies modernes (.NET 9, SignalR, RabbitMQ, etc.).

## Fonctionnalités
- **CRUD Événements** : Création et lecture d'événements (ex. : `{ id, type, timestamp, payload }`).
- **Notifications Temps Réel** : Diffusion des événements aux clients via SignalR.
- **Stats Basiques** : Comptage des événements par type (stocké dans ClickHouse ou Redis).
- **Traitement Asynchrone** : Envoi des événements à une queue RabbitMQ pour un traitement simulé (ex. : logging).

## Architecture
- **Clean Architecture** : Séparation claire entre domaine, application, et infrastructure.
- **DDD (Domain-Driven Design)** : Entité `Event` avec validation.
- **CQRS** : Utilisation de MediatR pour séparer les commandes (ex. : `CreateEventCommand`) et les requêtes (ex. : `GetEventsByTypeQuery`).

## Stack Technologique
- **Langage & Framework** : .NET 9 avec ASP.NET Core.
- **Bases de Données** :
  - MariaDB : Stockage principal des événements.
  - Redis : Caching des counts récents.
  - ClickHouse : Analyse des stats (OLAP).
- **Communication** :
  - REST API : Endpoints pour CRUD.
  - SignalR : Notifications en temps réel.
  - RabbitMQ : Gestion des queues asynchrones.
- **Tests** : Unitaires (xUnit) et d'intégration (WebApplicationFactory).

## Prérequis
- **Système** : Linux, macOS, ou Windows.
- **Dépendances** :
  - MariaDB (port 3306)
  - Redis (port 6379)
  - ClickHouse (port 8123)
  - RabbitMQ (ports 5672, 15672 pour l'interface web)
- **Outils** : .NET 9 SDK, `dotnet` CLI.
