# AI-Assisted Development



## Purpose



This project uses AI tools such as GitHub Copilot and coding agents to improve productivity without weakening architecture.



AI assistance is allowed, but generated code must follow the project rules.



## Golden rule



AI-generated code is not trusted by default.



Every generated change must be reviewed against:



- Clean Architecture rules

- Domain boundaries

- Testing requirements

- Naming conventions

- Security expectations



## What AI can help with



AI tools may help generate:



- Boilerplate

- Use case skeletons

- Tests

- Documentation

- Endpoint drafts

- EF Core configurations

- Architecture reviews

- Refactoring suggestions



## What AI must not decide alone



AI must not decide:



- Core domain rules

- Aggregate boundaries

- Security model

- Database strategy

- Authentication architecture

- Payment behavior

- Production deployment strategy



## Required workflow



Before asking an agent to generate code:



1\. State the layer being changed.

2\. State the intended use case.

3\. State whether persistence is involved.

4\. State whether tests are required.

5\. Ask the agent to review architecture impact.



## Bad prompts



Avoid vague prompts:



Create product stuff.

Add order logic.

Make API for inventory.

