# ADR 0003: Use AI-Assisted Development Setup



## Status



Accepted



## Context



The project will use AI tools such as GitHub Copilot and coding agents to speed up development.



Without repository-level instructions, AI tools may generate inconsistent code, violate architecture rules, or introduce unnecessary abstractions.



## Decision



Add repository-level AI development guidance using:



- `.github/copilot-instructions.md`

- `.github/instructions/\*`

- `.github/prompts/\*`

- architecture documentation

- pull request checklist

- architecture tests



## Consequences



Positive:



- More consistent AI-generated code.

- Better architecture compliance.

- Faster onboarding.

- Better review process.



Negative:



- Instructions must be maintained.

- Poor instructions can produce poor results.

- Generated code still requires human review.



## Rules



- AI-generated code must be reviewed.

- AI must not decide domain rules alone.

- AI must not introduce abstractions without justification.

- Generated code must pass build and tests.

