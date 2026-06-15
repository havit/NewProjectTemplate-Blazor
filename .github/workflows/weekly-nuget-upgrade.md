---
emoji: 🔄
description: Weekly Sunday-evening NuGet dependency upgrades with release-notes-driven migration updates.
on:
  schedule:
    - cron: "0 20 * * 0"
  workflow_dispatch:
permissions:
  contents: read
  issues: read
  pull-requests: read
strict: true
features:
  copilot-requests: false
network:
  allowed: [defaults, github, dotnet]
tools:
  github:
    mode: gh-proxy
    toolsets: [default]
    github-token: ${{ secrets.NUGET_UPGRADE_PAT }}
  cli-proxy: true
safe-outputs:
  create-pull-request:
    title-prefix: "[deps] "
    branch-prefix: "nuget/"
    labels: [dependencies, automated]
    draft: true
    if-no-changes: ignore
    allowed-files:
      - "Directory.Packages.props"
      - "**/*.csproj"
      - "**/*.props"
      - "**/*.targets"
      - "**/*.sln"
      - "**/*.json"
      - "**/*.config"
      - "**/*.cs"
      - "**/*.razor"
    protected-files:
      policy: request_review
      exclude:
        - "Directory.Packages.props"
  create-issue:
    title-prefix: "[deps] "
    labels: [dependencies, needs-manual-work]
---

# Weekly NuGet Upgrade

## Task

Every Sunday evening, review NuGet dependencies and prepare a safe upgrade.

1. Detect outdated NuGet packages in the repository (prioritize production projects; avoid test-only upgrades unless required transitively).
2. If no upgrades are available, call `noop` with a short summary.
3. For each candidate upgrade:
   - classify the version jump as patch, minor, or major
   - apply upgrade changes in project files
4. For each non-patch upgrade (minor or major), investigate official release notes/changelog and extract:
   - notable behavioral or API changes
   - required migration/code updates
   - deprecations/removals that affect this repository
5. Apply the required code changes in this repository so the upgraded packages compile cleanly.
6. Run repository commands needed for dependency and build verification.
7. If changes are ready, create exactly one draft PR with:
   - dependency changes
   - migration code updates
   - a markdown section `## Release notes impact (non-patch upgrades)` summarizing each non-patch package change and what was adjusted in code
   - a short risk checklist for any remaining manual validation
8. If upgrades are blocked (for example no compatible migration path can be completed safely), create an issue that explains:
   - attempted upgrades
   - blocking changes from release notes
   - concrete manual follow-up steps

## Constraints

- Keep the main job read-only and use only configured safe outputs for visible writes.
- Prefer official package sources, GitHub releases, and maintainer changelogs when collecting release notes.
- Keep changes minimal and focused on dependency upgrades and their required migrations.
- Never create multiple PRs in one run.
- Use `noop` when no visible output is needed.
