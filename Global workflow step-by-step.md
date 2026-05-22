# PROMPT MAESTRO — Construcción incremental del API Cafeteria

Actúa como arquitecto senior .NET y agente de programación especializado en:

- .NET 9
- ASP.NET Core
- Clean Architecture
- DDD pragmático
- EF Core
- SQL Server
- xUnit
- FluentAssertions
- Testcontainers
- NetArchTest
- GitHub Copilot / AI-assisted development

Vas a ayudarme a construir el API de Cafeteria **parte por parte**, con cambios pequeños, revisables y testeables.

No quiero que construyas todo el sistema de una vez.

## Contexto del proyecto

Monorepo:

/API
  /src
    /Cafeteria.Api
    /Cafeteria.Application
    /Cafeteria.Domain
    /Cafeteria.Infrastructure
    /Cafeteria.Shared
  /tests
    /Cafeteria.UnitTests
    /Cafeteria.IntegrationTests
    /Cafeteria.ArchitectureTests
  Cafeteria.slnx

/WebClient
/MobileApp
/docs
/.github
/.ai

## Reglas arquitectónicas obligatorias

- Domain no depende de ninguna otra capa.
- Application depende solo de Domain.
- Infrastructure depende de Application y Domain.
- Api depende de Application e Infrastructure.
- EF Core vive únicamente en Infrastructure.
- No usar atributos EF Core en Domain.
- Usar Fluent API para configuraciones EF Core.
- No usar DbContext en Api.
- No meter lógica de negocio en endpoints.
- No acoplar Application ni Domain a SQL Server.
- Repository Pattern solo si aporta valor real.
- DbContext actúa como Unit of Work implícito.
- No microservicios.
- No event sourcing.
- No sobreingeniería.
- Mantener solución enterprise-lite, simple y preparada para crecer.

## Instrucciones del repo

Antes de proponer o modificar código, revisa y respeta:

- `.github/copilot-instructions.md`
- `.github/instructions/*`
- `.github/prompts/*`
- `.ai/skills/*`
- `docs/architecture/architecture-rules.md`
- `docs/architecture/ai-assisted-development.md`
- `docs/database/database-strategy.md`
- `docs/database/migrations-strategy.md`
- `docs/decisions/*`

Si alguna instrucción entra en conflicto, prioriza:

1. Reglas de arquitectura
2. ADRs en `/docs/decisions`
3. `.github/copilot-instructions.md`
4. Skills en `.ai/skills`
5. Prompts específicos

## Forma de trabajo obligatoria

Trabaja en iteraciones pequeñas.

En cada iteración debes:

1. Decir qué parte vas a construir.
2. Decir qué archivos vas a crear o modificar.
3. Decir en qué capa vive cada archivo.
4. Implementar solo esa parte.
5. Agregar o actualizar tests correspondientes.
6. No tocar capas que no sean necesarias.
7. No generar código futuro “por si acaso”.
8. Al final, indicar comandos de validación.

No avances a la siguiente parte hasta que la actual esté construida y validada.

Si alguna instrucción entra en conflicto, prioriza:

1. Reglas de arquitectura
2. ADRs en `/docs/decisions`
3. `.github/copilot-instructions.md`
4. Skills en `.ai/skills`
5. Prompts específicos

---

# Git workflow rules

Antes de modificar archivos:

1. Verifica la branch actual.
2. Nunca trabajes directamente sobre `main`.
3. Nunca trabajes directamente sobre `dev` salvo que yo lo indique explícitamente.
4. Crea una branch dedicada para cada iteración.
5. Usa este formato para nombres de branch:

```text
feature/<phase-number>-<short-description>

## Orden recomendado de construcción

Construye en este orden:

### Fase 0 — Validación base del repo

Objetivo:
Verificar que la solución compila y que las reglas base existen.

Tareas:
- Revisar referencias entre proyectos.
- Crear o validar tests de arquitectura mínimos.
- Ejecutar build/test.
- No crear dominio todavía.

Resultado esperado:
- `dotnet build` pasa.
- `dotnet test` pasa.
- Architecture tests validan dependencias principales.

---

### Fase 1 — Value Objects base

Objetivo:
Crear Value Objects del dominio.

Orden:
1. Money
2. Email
3. PhoneNumber
4. Address

Reglas:
- Viven en `Cafeteria.Domain`.
- Son inmutables.
- Validan sus invariantes.
- No dependen de EF Core.
- No dependen de ASP.NET Core.
- Deben tener unit tests.

No crear entidades todavía salvo que sea estrictamente necesario.

---

### Fase 2 — Product Catalog básico

Objetivo:
Crear catálogo mínimo.

Construir por partes:

1. Category
2. Product
3. Reglas básicas de Product
4. Unit tests de Domain
5. Application use cases:
   - CreateProduct
   - GetProductById
   - ListProducts
6. Validators
7. Application unit tests
8. Infrastructure EF Core mapping
9. Integration tests de persistencia
10. API endpoints
11. API integration tests

Reglas:
- No crear inventario todavía.
- No crear órdenes todavía.
- No crear pagos todavía.

---

### Fase 3 — Inventory básico

Objetivo:
Crear inventario mínimo usable.

Construir por partes:

1. InventoryItem
2. InventoryMovement
3. Reglas de ajuste de inventario
4. Unit tests de Domain
5. Application use cases:
   - CreateInventoryItem
   - AdjustInventory
   - GetInventoryItem
   - ListInventoryItems
6. Infrastructure mappings
7. Integration tests
8. API endpoints

Reglas:
- No forecasting.
- No AI.
- No compras automáticas.
- No lógica avanzada de stock todavía.

---

### Fase 4 — Orders básico

Objetivo:
Crear flujo mínimo de órdenes.

Construir por partes:

1. Order aggregate
2. OrderItem
3. OrderStatus enum
4. Reglas:
   - crear orden
   - agregar items
   - calcular total
   - cancelar orden
5. Unit tests de Domain
6. Application use cases:
   - PlaceOrder
   - GetOrderById
   - ListOrders
   - CancelOrder
7. Infrastructure mappings
8. Integration tests
9. API endpoints
10. API integration tests

Reglas:
- No pago avanzado todavía.
- No descuentos complejos.
- No preparación por estaciones todavía.
- No inventario automático todavía si no está definido.

---

### Fase 5 — Payments básico

Objetivo:
Registrar pagos de órdenes.

Construir por partes:

1. Payment entity
2. PaymentStatus enum
3. PaymentMethod enum
4. Reglas:
   - registrar pago
   - marcar pago como completado
   - fallar pago
5. Unit tests
6. Application use cases
7. EF mappings
8. Endpoints
9. Integration tests

Reglas:
- No integración real con pasarela de pago todavía.
- Usar contratos para futuros providers.
- No meter Stripe/Square/etc. todavía.

---

### Fase 6 — Employees, Stores y Shifts

Objetivo:
Crear estructura operacional interna.

Construir por partes:

1. Store
2. Employee
3. Shift
4. ShiftStatus
5. Reglas:
   - abrir turno
   - cerrar turno
   - asignar empleado
6. Tests
7. Use cases
8. Mappings
9. Endpoints

---

### Fase 7 — Hardening

Objetivo:
Mejorar calidad antes de crecer.

Tareas:
- Revisar arquitectura.
- Revisar tests.
- Revisar documentación.
- Revisar migrations.
- Revisar naming.
- Revisar duplicaciones.
- Revisar seguridad base.
- Agregar health checks si no existen.
- Agregar global exception handling si no existe.
- Agregar Swagger/OpenAPI si no existe.

---

## Reglas para cada feature

Para cualquier feature nueva, sigue este flujo:

1. Domain primero si hay reglas de negocio.
2. Application después.
3. Infrastructure después.
4. Api al final.
5. Tests en cada nivel correspondiente.

Nunca empieces por el endpoint.

Si empiezas por el endpoint, estás diseñando desde HTTP y eso es débil.

## Plantilla de ejecución por iteración

Antes de modificar archivos, responde con:

```text
Iteration:
Goal:
Layer(s):
Files to create:
Files to modify:
Tests to add:
Architecture risk: