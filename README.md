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

| Type    | Description                              |
|---------|------------------------------------------|
| `feat`  | New feature added                        |
| `fix`   | Bug fix or issue resolution              |
| `opt`   | Code optimization or performance boost   |
| `perf`  | Performance improvement                  |
| `ref`   | Code refactoring without behavior change |
| `docs`  | Documentation updates                    |
| `test`  | Changes to tests or test setup           |
| `chore` | Maintenance, cleanup, or minor tweaks    |

## ✅ Example Commit Identifiers

## ✅ General Guidelines

- Keep commit messages **concise and action-driven**.
- Use **lowercase type identifiers** (e.g., `feat`, `fix`).
- Indicate **pre-release versioning** with `(pre-vX.Y.Z)`.
- Ensure meaningful messages to maintain a **clean commit history**.

## ✅ When a Commit is Related to an Issue

- If a commit **fixes, addresses, or references** an issue, **add the issue number at the end**.
- Use `#issue-number` format to **automatically link** the commit to the issue tracker.

--- 

# 🌿 Branching Strategy

## **Main Branches:**
- `main` → Holds stable production-ready code but does not directly trigger packaging.
- `develop` → Active development branch where new features are merged before tagging.

## **Feature & Fix Branches:**
- `feature/{feature-name}` → Used for developing new features.
- `fix/{issue-name}` → Used for resolving bugs and patches.

## **Tag-Based Packaging & Releases:**
- Releases are **only generated from Git tags**.
- Once a release is ready, a **version tag (`vX.Y.Z`)** is created on `main`.
- The CI/CD pipeline detects the new tag and triggers packaging automatically.
- Hotfixes follow the same process but are tagged separately (`vX.Y.Z-hotfix`).

## **Branching Workflow:**
1. Create a `feature/{feature-name}` branch from `develop` and implement changes.
2. Merge feature branches into `develop` via pull requests.
3. Once stable, merge `develop` into `main`.
4. Tag the `main` branch with a new version (`vX.Y.Z`).
5. The packaging system detects the tag and triggers a release build.  

