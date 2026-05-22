# Pull Request


## Summary

Describe what changed and why.

## Type of change

[ ] Feature
[ ] Bug fix
[ ] Refactor
[ ] Test
[ ] Documentation
[ ] Infrastructure

## Architecture checklist

[ ] Domain does not depend on Application, Infrastructure, or Api.
[ ] Application depends only on Domain.
[ ] Infrastructure depends only on Application and Domain.
[ ] Api depends only on Application and Infrastructure.
[ ] No EF Core types leaked into Domain or Application.
[ ] No SQL Server-specific logic leaked outside Infrastructure.
[ ] No business logic was added to endpoints/controllers.
[ ] No unnecessary abstraction was introduced.


## Testing checklist

[ ] Unit tests added or updated.
[ ] Integration tests added or updated if persistence/API behavior changed.
[ ] Architecture tests still pass.
[ ] `dotnet build` passes.
[ ] `dotnet test` passes.


## Documentation checklist

[ ] Relevant docs updated.
[ ] ADR added if an architectural decision was made.
[ ] API documentation updated if endpoints changed.


## Notes

Mention risks, tradeoffs, or follow-up work.
