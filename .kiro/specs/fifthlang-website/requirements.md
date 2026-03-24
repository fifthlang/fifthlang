# Requirements Document

## Introduction

This document specifies the requirements for a complete redesign of the fifthlang.org website. The current site uses MkDocs with a ReadTheDocs theme, which serves documentation adequately but fails as a landing portal for first-time visitors. The new website must present Fifth as an appealing, modern programming language with first-class knowledge graph capabilities, inspire newcomers to invest time learning it, and remain easy to maintain by a single developer. The site must be deployable via GitHub Pages.

## Glossary

- **Website**: The public-facing fifthlang.org site, encompassing all pages from the landing page through documentation, blog, and tutorials.
- **Landing_Page**: The root page (index) of the Website, serving as the primary entry point for all visitors.
- **Visitor**: Any person accessing the Website, including first-time visitors, returning developers, and potential contributors.
- **Content_Author**: The single developer maintaining the Website, who adds and updates content over time.
- **Static_Site_Generator**: The build tool that transforms source content (Markdown, templates, configuration) into static HTML/CSS/JS files for deployment.
- **Navigation_System**: The menus, breadcrumbs, and links that allow Visitors to move between sections of the Website.
- **Code_Snippet_Block**: A syntax-highlighted block of Fifth source code displayed on the Website.
- **Hero_Section**: The prominent top area of the Landing_Page that communicates the language's value proposition.
- **Call_To_Action**: A prominent UI element (button, link, banner) that directs Visitors toward a specific next step such as installation, trying the language, or contributing.
- **GitHub_Pages**: GitHub's static site hosting service used to publish the Website from a repository branch or GitHub Actions workflow.

## Requirements

### Requirement 1: Landing Page Impact

**User Story:** As a first-time Visitor, I want to immediately understand what Fifth is and why it is interesting, so that I am motivated to explore further.

#### Acceptance Criteria

1. WHEN a Visitor loads the Landing_Page, THE Website SHALL display a Hero_Section containing a concise tagline that communicates Fifth's unique value proposition (systems programming with native knowledge graph support) within the first visible viewport.
2. WHEN a Visitor loads the Landing_Page, THE Website SHALL display at least three bite-sized feature highlight cards below the Hero_Section, each summarizing a distinct Fifth capability in no more than two sentences with an accompanying Code_Snippet_Block.
3. THE Landing_Page SHALL include at least two Call_To_Action elements: one directing Visitors to the installation guide and one directing Visitors to the "Learn Fifth in Y Minutes" tutorial.
4. WHEN a Visitor loads the Landing_Page, THE Website SHALL display a short (3-6 line) interactive-style Code_Snippet_Block in the Hero_Section demonstrating Fifth's knowledge graph syntax.
5. THE Landing_Page SHALL render fully within 3 seconds on a standard broadband connection without requiring JavaScript for initial content display.

### Requirement 2: Site Navigation and Information Architecture

**User Story:** As a Visitor, I want clear and consistent navigation across all sections of the Website, so that I can find content without confusion.

#### Acceptance Criteria

1. THE Navigation_System SHALL provide a persistent top-level menu visible on every page, containing links to: Home, Getting Started, Documentation, Tutorials, Blog, and Get Involved.
2. THE Navigation_System SHALL include breadcrumb navigation on all pages except the Landing_Page, showing the current page's position within the site hierarchy.
3. WHEN a Visitor accesses the Website on a device with a viewport width below 768 pixels, THE Navigation_System SHALL collapse into a responsive hamburger menu.
4. THE Navigation_System SHALL highlight the currently active section in the top-level menu.
5. THE Website SHALL include a site-wide search feature that indexes all textual content across documentation, tutorials, and blog posts.

### Requirement 3: Getting Started Section

**User Story:** As a newcomer to Fifth, I want a clear onboarding path with installation instructions and a first-program walkthrough, so that I can start using Fifth within minutes.

#### Acceptance Criteria

1. THE Website SHALL provide a Getting Started section containing, at minimum, the following pages in sequential order: Installation, Your First Program, and Language Tour.
2. THE Getting Started section SHALL present installation instructions for Linux, macOS, and Windows, each with platform-specific commands and verification steps.
3. WHEN a Visitor completes the "Your First Program" page, THE Website SHALL link to the next logical step (Language Tour or Knowledge Graphs guide).
4. THE Getting Started section SHALL include copy-to-clipboard functionality on all Code_Snippet_Blocks containing shell commands or Fifth source code.

### Requirement 4: Documentation Section

**User Story:** As a developer using Fifth, I want comprehensive and well-organized reference documentation, so that I can look up language features, APIs, and compiler options efficiently.

#### Acceptance Criteria

1. THE Website SHALL provide a Documentation section containing, at minimum: Language Reference, Knowledge Graphs Guide, SDK Reference, and Compiler CLI Reference.
2. THE Documentation section SHALL organize content with a sidebar navigation showing all documentation pages grouped by category.
3. WHEN a Visitor navigates within the Documentation section, THE Website SHALL maintain the sidebar scroll position and expansion state.
4. THE Documentation section SHALL support linking to specific headings within any documentation page via anchor URLs.
5. THE Website SHALL migrate all existing content from the current MkDocs documentation (Getting Started, Development, Design Documents) into the new Documentation section without loss of information.

### Requirement 5: Tutorials Section

**User Story:** As a Visitor wanting to learn Fifth in depth, I want structured tutorials that build on each other progressively, so that I can develop practical skills with the language.

#### Acceptance Criteria

1. THE Website SHALL provide a Tutorials section separate from the Documentation section, containing guided, task-oriented learning content.
2. THE Tutorials section SHALL include at minimum: a "Learn Fifth in Y Minutes" quick-start tutorial and a "Working with Knowledge Graphs" tutorial.
3. WHEN a Visitor completes a tutorial, THE Website SHALL display a link to the next recommended tutorial or related documentation page.
4. THE Tutorials section SHALL present each tutorial with an estimated reading time and a list of prerequisites.

### Requirement 6: Blog Section

**User Story:** As a Visitor, I want to read blog posts about Fifth's development progress, design decisions, and release announcements, so that I can follow the project's evolution.

#### Acceptance Criteria

1. THE Website SHALL provide a Blog section that displays posts in reverse chronological order with title, date, author, and summary.
2. THE Blog section SHALL migrate all existing blog posts from the current MkDocs site (Announcing Fifth, Release Packaging Pipeline, The Story of Fifth, Graph Assertion Block).
3. THE Blog section SHALL support an RSS/Atom feed that Visitors can subscribe to for new post notifications.
4. WHEN a Visitor views a blog post, THE Website SHALL display estimated reading time and publication date.

### Requirement 7: Community and Get Involved Section

**User Story:** As a Visitor interested in contributing to Fifth, I want to know how to get involved, so that I can participate in the project's growth.

#### Acceptance Criteria

1. THE Website SHALL provide a "Get Involved" section framed as a call to action for newcomers to join and shape the project, rather than describing an existing community.
2. THE Get Involved section SHALL include direct links to: the GitHub repository, GitHub Discussions, GitHub Issues, and contribution guidelines.
3. THE Get Involved section SHALL present specific areas where contributions are welcome (language design feedback, documentation, bug reports, feature proposals) as actionable items.
4. THE Get Involved section SHALL communicate that Fifth is developed by a single developer and that early contributors have an outsized opportunity to shape the language.

### Requirement 8: Code Presentation and Syntax Highlighting

**User Story:** As a Visitor reading code examples, I want Fifth code to be clearly formatted and syntax-highlighted, so that I can read and understand code snippets easily.

#### Acceptance Criteria

1. THE Website SHALL render all Fifth Code_Snippet_Blocks with syntax highlighting that distinguishes keywords, types, strings, comments, and knowledge graph constructs (IRIs, triple literals, SPARQL blocks).
2. THE Website SHALL provide copy-to-clipboard functionality on all Code_Snippet_Blocks.
3. WHEN a Code_Snippet_Block exceeds 20 lines, THE Website SHALL display it in a scrollable container with visible line numbers.
4. THE Website SHALL support a custom syntax definition for Fifth that can be maintained alongside the site source.

### Requirement 9: Visual Design and Branding

**User Story:** As a Visitor, I want the Website to look modern, clean, and professional, so that I perceive Fifth as a credible and well-maintained project.

#### Acceptance Criteria

1. THE Website SHALL use a consistent color palette, typography, and spacing system across all pages. THE color palette SHALL be derived from the primary colors in the Fifth logo (`#FF0082` magenta/pink and `#FE5B00` warm orange), using them as accent and highlight colors with appropriate neutral tones for backgrounds and text.
2. THE Website SHALL incorporate the existing Fifth logo (fifth-logo.svg, fifth-logo.shaded.svg) in the site header and favicon.
3. THE Website SHALL use a responsive layout that renders correctly on viewports from 320px to 2560px wide.
4. THE Website SHALL use a light color scheme as the default, with an optional dark mode toggle.
5. THE Website SHALL achieve a Lighthouse accessibility score of 90 or above.

### Requirement 10: Static Site Generator Selection

**User Story:** As the Content_Author, I want to choose a reliable, well-documented static site generator that simplifies content management, so that I can add and update content without fighting the tooling.

#### Acceptance Criteria

1. THE Static_Site_Generator SHALL produce static HTML/CSS/JS output deployable to GitHub_Pages without server-side processing.
2. THE Static_Site_Generator SHALL support Markdown as the primary content authoring format.
3. THE Static_Site_Generator SHALL support custom layouts and templates for different page types (landing page, documentation, blog post, tutorial).
4. THE Static_Site_Generator SHALL have an active maintenance community, stable release history, and comprehensive official documentation.
5. THE Website design document SHALL evaluate and present at least three Static_Site_Generator options with trade-offs, recommending one for implementation.


### Requirement 11: GitHub Pages Deployment

**User Story:** As the Content_Author, I want the Website to deploy automatically to GitHub Pages on every push to the main branch, so that publishing new content requires no manual steps beyond merging a pull request.

#### Acceptance Criteria

1. THE Website SHALL be deployable to GitHub_Pages using a GitHub Actions workflow triggered on pushes to the main branch.
2. THE Website SHALL serve the custom domain fifthlang.org (or fifth-lang.org) via a CNAME record configured in the repository.
3. THE Website build process SHALL complete within 5 minutes on GitHub Actions runners.
4. IF the Website build fails, THEN THE GitHub Actions workflow SHALL report the failure and prevent deployment of broken content.

### Requirement 12: Content Authoring Experience

**User Story:** As the Content_Author, I want to add new pages, blog posts, and tutorials using simple Markdown files with minimal frontmatter, so that content creation is fast and low-friction.

#### Acceptance Criteria

1. THE Website SHALL support adding a new blog post by creating a single Markdown file with date, title, and author in YAML frontmatter.
2. THE Website SHALL support adding a new documentation page by creating a Markdown file in the appropriate directory and optionally updating a navigation configuration.
3. THE Website SHALL support adding a new tutorial by creating a Markdown file with frontmatter specifying title, estimated reading time, and prerequisites.
4. THE Website SHALL provide a local development server that the Content_Author can run to preview changes before pushing.
5. THE Website SHALL support hot-reload during local development so that content changes appear without restarting the server.

### Requirement 13: SEO and Discoverability

**User Story:** As the Content_Author, I want the Website to be discoverable by search engines, so that developers searching for knowledge graph programming or Fifth language can find the site.

#### Acceptance Criteria

1. THE Website SHALL generate a sitemap.xml file listing all public pages.
2. THE Website SHALL include appropriate meta tags (title, description, Open Graph, Twitter Card) on every page.
3. THE Website SHALL use semantic HTML elements (header, nav, main, article, footer) for page structure.
4. THE Website SHALL generate clean, human-readable URLs without file extensions or unnecessary path segments.

### Requirement 14: Content Migration

**User Story:** As the Content_Author, I want only content that serves the Website's purpose migrated to the new site, so that the public-facing site contains only material useful to Visitors rather than carrying over internal or outdated content.

#### Acceptance Criteria

1. WHEN deciding whether to migrate existing content, THE Content_Author SHALL evaluate each piece against the criterion: "Does this content serve the Website's purpose of attracting, onboarding, or supporting Visitors?" Only content meeting this criterion SHALL be migrated.
2. THE Website SHALL incorporate existing content (e.g., "Learn Fifth in Y Minutes", installation guide, knowledge graphs guide, blog posts) into the appropriate sections only when that content is useful to Visitors in its new context.
3. THE Website SHALL NOT migrate internal developer documentation, design documents, or development process content that serves contributors rather than Visitors.
4. WHEN existing content is migrated, THE Website SHALL update all internal cross-references and links to use the new URL structure.
5. THE existing `docs/` folder SHALL remain in the repository and transition to serving as internal developer documentation, separate from the public Website content.

### Requirement 15: Repository Structure and Content Separation

**User Story:** As the Content_Author, I want the new website source to live in a dedicated root folder separate from the existing internal docs, so that public website content and internal developer documentation are clearly distinguished.

#### Acceptance Criteria

1. THE Website source content (templates, pages, assets, configuration) SHALL reside under a new root-level `/website` folder in the repository.
2. THE existing `docs/` folder SHALL remain unchanged and continue to serve as internal developer documentation, not as public website content.
3. THE Static_Site_Generator build process SHALL use `/website` as its source root.
4. THE GitHub Actions deployment workflow SHALL build and deploy exclusively from the `/website` folder.

### Requirement 16: Performance and Reliability

**User Story:** As a Visitor, I want the Website to load quickly and work reliably, so that my experience is smooth regardless of my device or connection.

#### Acceptance Criteria

1. THE Website SHALL achieve a Lighthouse performance score of 90 or above on the Landing_Page.
2. THE Website SHALL function with core content readable when JavaScript is disabled.
3. THE Website SHALL not depend on external CDNs for critical rendering resources (fonts, CSS framework); all critical assets SHALL be bundled with the site.
4. THE Website SHALL include a custom 404 error page that provides navigation back to the Landing_Page and a search option.
