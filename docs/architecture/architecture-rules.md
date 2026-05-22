# Architecture Rules



## Purpose



This document defines the non-negotiable architecture rules for the Cafeteria platform.



## Project dependency rules



Allowed dependencies:



Cafeteria.Domain

&#x20; depends on nothing



Cafeteria.Application

&#x20; depends on Cafeteria.Domain



Cafeteria.Infrastructure

&#x20; depends on Cafeteria.Application

&#x20; depends on Cafeteria.Domain



Cafeteria.Api

&#x20; depends on Cafeteria.Application

&#x20; depends on Cafeteria.Infrastructure

