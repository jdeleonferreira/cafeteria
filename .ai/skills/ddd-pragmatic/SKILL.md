\# Skill: Pragmatic DDD



\## Purpose



Use this skill when working with domain concepts, entities, value objects, aggregates, and domain events.



The project uses pragmatic DDD, not academic DDD.



\## Domain principles



Domain code must express business behavior clearly.



Prefer:



\- Meaningful methods

\- Protected invariants

\- Explicit state transitions

\- Small aggregates

\- Simple value objects



Avoid:



\- Anemic models when behavior clearly belongs in the domain

\- Huge aggregates

\- Premature domain events

\- Event sourcing

\- Complex patterns without current need



\## Entities



Entities should:



\- Have identity

\- Protect business invariants

\- Expose behavior through methods

\- Avoid public setters when possible

\- Avoid persistence attributes



Example behavior style:



```text

order.MarkAsPaid(...)

inventoryItem.RegisterMovement(...)

shift.Close(...)

