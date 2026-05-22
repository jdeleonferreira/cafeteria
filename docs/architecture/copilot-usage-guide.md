# Copilot Usage Guide



## Purpose



This guide explains how to use Copilot and coding agents effectively in this repository.



## Repository context



Copilot should use:



- `.github/copilot-instructions.md`

- `.github/instructions/\*`

- `.github/prompts/\*`

- `/docs/architecture/\*`

- existing code patterns



## Recommended workflow



For any new feature:



1\. Ask for an architecture review.

2\. Ask for the Application use case.

3\. Ask for Domain changes only if needed.

4\. Ask for Infrastructure implementation only after contracts are stable.

5\. Ask for API endpoint only after use case exists.

6\. Ask for tests.

7\. Run build and tests.



## Prompt examples



### Create use case





Create a use case for placing an order.



Generate only Application layer files:

- command

- handler

- validator

- response DTO if needed



Do not modify Api or Infrastructure.

Do not use EF Core.

Respect Clean Architecture.