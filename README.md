# 📝 Commit Message Guidelines

To maintain a **clean, structured, and enterprise-grade commit history**, follow this commit format for all changes.

## ✅ Commit Format
```
[type]: [project/component] [short description] (pre-v[version]) [#issue-number]

# Example:
feat: messaging/email new template support (pre-v1.0.1) #123 
fix: messaging/email placeholder bug (pre-v1.0.1) #98
```

## ✅ Allowed Commit Types
| Type  | Description |
|-------|-------------------------------------------|
| `feat` | New feature added |
| `fix`  | Bug fix or issue resolution |
| `opt`  | Code optimization or performance boost |
| `perf` | Performance improvement |
| `ref`  | Code refactoring without behavior change |
| `docs` | Documentation updates |
| `test` | Changes to tests or test setup |
| `chore`| Maintenance, cleanup, or minor tweaks |

## ✅ Example Commit Identifiers

## ✅ General Guidelines
- Keep commit messages **concise and action-driven**.
- Use **lowercase type identifiers** (e.g., `feat`, `fix`).
- Indicate **pre-release versioning** with `(pre-vX.Y.Z)`.
- Ensure meaningful messages to maintain a **clean commit history**.


## ✅ When a Commit is Related to an Issue
- If a commit **fixes, addresses, or references** an issue, **add the issue number at the end**.
- Use `#issue-number` format to **automatically link** the commit to the issue tracker.
- Example:  
