# Implementation Plan: fifthlang-website

## Overview

Build the fifthlang.org public website using Astro v5.x under the `/website` root folder. The implementation proceeds incrementally: project scaffolding → content layer → layouts → components → pages → content migration → search & dark mode → CI/CD → testing. Each task builds on the previous, ensuring no orphaned code.

## Tasks

- [ ] 1. Scaffold Astro project and configure build tooling
  - [ ] 1.1 Initialize Astro project under `/website`
    - Run `npm create astro@latest` in `/website` (or manually create `package.json`, `astro.config.mjs`, `tsconfig.json`)
    - Install dependencies: `astro`, `@astrojs/mdx`, `@astrojs/sitemap`
    - Install dev dependencies: `vitest`, `fast-check`, `cheerio`
    - Add `website/node_modules` to root `.gitignore`
    - _Requirements: 10.1, 10.2, 10.3, 10.4, 15.1, 15.3_

  - [ ] 1.2 Configure `astro.config.mjs` with site URL, integrations, and Fifth syntax highlighting
    - Set `site: 'https://fifthlang.org'`, `outDir: './dist'`
    - Register `mdx()` and `sitemap()` integrations
    - Configure Shiki with `github-light` / `github-dark` themes
    - Load custom Fifth TextMate grammar via `shikiConfig.langs`
    - _Requirements: 8.1, 8.4, 10.1, 13.1_

  - [ ] 1.3 Create the Fifth TextMate grammar file (`website/fifth.tmLanguage.json`)
    - Define scopes for keywords, types, strings, comments, knowledge graph constructs (IRIs, triple literals, SPARQL blocks), and operators
    - Ensure at least three distinct token categories are differentiated
    - _Requirements: 8.1, 8.4_

  - [ ] 1.4 Create `public/` directory with static assets
    - Copy `fifth-logo.svg`, `fifth-logo.shaded.svg` into `website/public/`
    - Create `favicon.svg` (or derive from logo)
    - Create `CNAME` file with `fifthlang.org`
    - _Requirements: 9.2, 11.2_

- [ ] 2. Define content collections and design tokens
  - [ ] 2.1 Create content collection schemas (`src/content/config.ts`)
    - Define `docs` collection with Zod schema: `title` (string), `description` (string, optional), `order` (number, optional), `category` (string, optional)
    - Define `blog` collection with Zod schema: `title` (string), `date` (date), `author` (string, default), `summary` (string), `draft` (boolean, default false)
    - Define `tutorials` collection with Zod schema: `title` (string), `description` (string, optional), `readingTime` (string), `prerequisites` (array of strings, default []), `order` (number, optional), `nextTutorial` (string, optional)
    - Export all collections
    - _Requirements: 12.1, 12.2, 12.3_

  - [ ]* 2.2 Write property test for content collection schemas (Property 9)
    - **Property 9: Content collection schema validation**
    - Use fast-check to generate random valid frontmatter objects for each content type and verify Zod schema acceptance
    - Generate invalid frontmatter (missing required fields, wrong types) and verify rejection
    - **Validates: Requirements 12.1, 12.2, 12.3**

  - [ ] 2.3 Create global CSS with design tokens (`src/styles/global.css`)
    - Define CSS custom properties for colors (accent magenta `#FF0082`, accent orange `#FE5B00`, derived palette, neutrals)
    - Define dark mode overrides under `[data-theme="dark"]`
    - Define typography tokens (`--font-sans`, `--font-mono`, sizes, line heights)
    - Define spacing scale (`--space-1` through `--space-16`)
    - Add base reset and global styles
    - _Requirements: 9.1, 9.4_

  - [ ] 2.4 Create navigation configuration (`src/config/nav.ts`)
    - Define `docCategories` array with slug/label pairs for sidebar ordering
    - Define top-level nav items array (Home, Getting Started, Documentation, Tutorials, Blog, Get Involved)
    - _Requirements: 2.1, 4.2_

- [ ] 3. Checkpoint — Verify project builds and schemas validate
  - Ensure `astro build` succeeds with empty content directories
  - Ensure all tests pass, ask the user if questions arise.

- [ ] 4. Build layout components
  - [ ] 4.1 Create `BaseLayout.astro`
    - Accept `title`, `description` props
    - Render `<html>` with `data-theme` attribute for dark mode
    - Include `<head>` with meta tags: `<title>`, `<meta name="description">`, Open Graph (`og:title`, `og:description`), Twitter Card (`twitter:card`, `twitter:title`)
    - Import `global.css`
    - Inline dark mode initialization script in `<head>` (read `localStorage`, set `data-theme` before paint)
    - Render semantic structure: `<header>` (with Header component), `<main>` (slot), `<footer>` (with Footer component)
    - Include Pagefind search script (async)
    - _Requirements: 13.2, 13.3, 16.2_

  - [ ] 4.2 Create `DocLayout.astro`
    - Extend `BaseLayout` with sidebar + content area
    - Include `DocSidebar` component on the left
    - Include `Breadcrumbs` component above content
    - Render content slot in `<main>` / `<article>`
    - Add previous/next page navigation links
    - _Requirements: 4.2, 4.3, 2.2_

  - [ ] 4.3 Create `BlogLayout.astro`
    - Extend `BaseLayout` for blog post pages
    - Display publication date, author, estimated reading time
    - Include back-to-blog-list link
    - _Requirements: 6.4_

  - [ ] 4.4 Create `TutorialLayout.astro`
    - Extend `BaseLayout` for tutorial pages
    - Display reading time and prerequisites list
    - Include next tutorial link from frontmatter `nextTutorial` field
    - _Requirements: 5.3, 5.4_

- [ ] 5. Build UI components
  - [ ] 5.1 Create `Header.astro` and `Nav.astro`
    - Render logo in header linking to home
    - Render persistent top-level nav with links: Home, Getting Started, Documentation, Tutorials, Blog, Get Involved
    - Highlight active section based on current URL path
    - Implement responsive hamburger menu for viewports below 768px (CSS-only or minimal JS)
    - Include dark mode toggle button and search trigger button
    - _Requirements: 2.1, 2.3, 2.4, 9.2_

  - [ ] 5.2 Create `Footer.astro`
    - Render site footer with copyright, links to GitHub, and key site sections
    - Use `<footer>` semantic element
    - _Requirements: 13.3_

  - [ ] 5.3 Create `Breadcrumbs.astro`
    - Accept current page path, generate breadcrumb trail from URL segments
    - Render as `<nav aria-label="Breadcrumb">` with ordered list
    - Do not render on the landing page (index)
    - _Requirements: 2.2_

  - [ ] 5.4 Create `DocSidebar.astro`
    - Query docs collection, group by `category` field, sort by `order`
    - Render collapsible category groups
    - Highlight current page
    - Preserve scroll/expansion state via `sessionStorage` (client-side script)
    - _Requirements: 4.2, 4.3_

  - [ ] 5.5 Create `CodeBlock.astro`
    - Wrap Astro's built-in Shiki code output
    - Add copy-to-clipboard button on every code block
    - For blocks exceeding 20 lines: add visible line numbers and scrollable container
    - _Requirements: 3.4, 8.2, 8.3_

  - [ ] 5.6 Create `HeroSection.astro`
    - Display tagline: "A systems programming language with native knowledge graphs"
    - Include a 3-6 line Fifth code snippet demonstrating KG syntax
    - Include two CTAs: "Install Fifth" → installation guide, "Learn in Y Minutes" → tutorial
    - _Requirements: 1.1, 1.3, 1.4_

  - [ ] 5.7 Create `FeatureCard.astro`
    - Accept props: icon/illustration, title, description (1-2 sentences), code snippet
    - Render as a card component
    - _Requirements: 1.2_

  - [ ] 5.8 Create `ThemeToggle.astro`
    - Toggle `data-theme` attribute on `<html>` between light/dark
    - Store preference in `localStorage`
    - Respect `prefers-color-scheme` as default when no stored preference
    - _Requirements: 9.4_

  - [ ] 5.9 Create `SearchWidget.astro`
    - Integrate Pagefind search UI
    - Support keyboard shortcut (Cmd/Ctrl+K) to open search modal
    - Degrade gracefully when JS is disabled (hide search)
    - _Requirements: 2.5_

- [ ] 6. Checkpoint — Verify components render in isolation
  - Ensure `astro build` succeeds with all components and layouts
  - Ensure all tests pass, ask the user if questions arise.

- [ ] 7. Create page routes
  - [ ] 7.1 Create landing page (`src/pages/index.astro`)
    - Use `BaseLayout` (not DocLayout)
    - Compose `HeroSection`, three `FeatureCard` components (Imperative Programming, Knowledge Graphs, .NET Integration), and any additional landing content
    - Ensure all content renders in static HTML without JS
    - _Requirements: 1.1, 1.2, 1.3, 1.4, 1.5_

  - [ ] 7.2 Create documentation pages (`src/pages/docs/[...slug].astro`)
    - Use `DocLayout`
    - Query `docs` content collection, render each entry with `getStaticPaths`
    - Generate heading anchor IDs for all `h1`–`h6` elements
    - _Requirements: 4.1, 4.4_

  - [ ] 7.3 Create blog pages (`src/pages/blog/index.astro`, `src/pages/blog/[...slug].astro`)
    - Blog index: list posts in reverse chronological order with title, date, author, summary
    - Blog post pages: use `BlogLayout`, render content
    - Filter out `draft: true` posts in production builds
    - _Requirements: 6.1, 6.4_

  - [ ] 7.4 Create RSS feed endpoint (`src/pages/blog/rss.xml.ts`)
    - Use `@astrojs/rss` package to generate RSS feed from blog collection
    - Include title, date, summary, and link for each post
    - _Requirements: 6.3_

  - [ ] 7.5 Create tutorial pages (`src/pages/tutorials/index.astro`, `src/pages/tutorials/[...slug].astro`)
    - Tutorial index: list tutorials with title, reading time, description
    - Tutorial pages: use `TutorialLayout`, render content
    - _Requirements: 5.1, 5.2_

  - [ ] 7.6 Create Get Involved page (`src/pages/get-involved.astro`)
    - Use `BaseLayout`
    - Frame as call to action for newcomers to join and shape the project
    - Include links to GitHub repo, Discussions, Issues, contribution guidelines
    - Present specific contribution areas as actionable items
    - Communicate single-developer status and outsized contributor opportunity
    - _Requirements: 7.1, 7.2, 7.3, 7.4_

  - [ ] 7.7 Create custom 404 page (`src/pages/404.astro`)
    - Use `BaseLayout`
    - Provide navigation back to home and search option
    - _Requirements: 16.4_

- [ ] 8. Migrate content from `docs/` to website content collections
  - [ ] 8.1 Migrate Getting Started content
    - Copy and adapt `docs/Getting-Started/installation.md` → `src/content/docs/getting-started/installation.md`
    - Create `src/content/docs/getting-started/first-program.md` (new content or adapted from existing)
    - Copy and adapt `docs/Getting-Started/full-project-setup.md` → `src/content/docs/getting-started/project-setup.md`
    - Add appropriate frontmatter (`title`, `category`, `order`) to each file
    - Update all internal links to new URL structure
    - _Requirements: 3.1, 3.2, 3.3, 14.1, 14.2, 14.4_

  - [ ] 8.2 Migrate tutorials
    - Copy and adapt `docs/Getting-Started/learn5thInYMinutes.md` → `src/content/tutorials/learn-fifth-in-y-minutes.md`
    - Copy and adapt `docs/Getting-Started/knowledge-graphs.md` → `src/content/tutorials/working-with-knowledge-graphs.md` (or `src/content/docs/knowledge-graphs/index.md` per design)
    - Add tutorial frontmatter (`title`, `readingTime`, `prerequisites`, `order`, `nextTutorial`)
    - _Requirements: 5.1, 5.2, 14.1, 14.2_

  - [ ] 8.3 Migrate blog posts
    - Copy and reformat all 4 blog posts from `docs/Blog/` to `src/content/blog/`
    - Convert frontmatter to blog schema format (`title`, `date`, `author`, `summary`, `draft`)
    - Update internal links
    - _Requirements: 6.2, 14.2, 14.4_

  - [ ] 8.4 Migrate SDK reference content
    - Copy and adapt `docs/Designs/fifth-sdk-readme.md` → `src/content/docs/sdk-reference/index.md`
    - Add documentation frontmatter
    - Update internal links
    - _Requirements: 4.1, 14.1, 14.2_

  - [ ] 8.5 Create placeholder documentation pages for sections without existing source
    - Create `src/content/docs/language-reference/index.md` with stub content
    - Create `src/content/docs/compiler-cli/index.md` with stub content
    - Add appropriate frontmatter to each
    - _Requirements: 4.1_

- [ ] 9. Checkpoint — Verify full site builds with migrated content
  - Ensure `astro build` succeeds and all pages render
  - Verify migrated content appears at correct URLs
  - Verify internal dev docs (Development, Planning, Designs except SDK) are NOT present in output
  - Ensure all tests pass, ask the user if questions arise.

- [ ] 10. Implement search integration (Pagefind)
  - [ ] 10.1 Install and configure Pagefind
    - Add `pagefind` as a dev dependency
    - Add post-build script: `npx pagefind --site dist` to `package.json`
    - Wire `SearchWidget.astro` to load Pagefind UI from built index
    - _Requirements: 2.5_

- [ ] 11. Create GitHub Actions deployment workflow
  - [ ] 11.1 Create `.github/workflows/deploy-website.yml`
    - Trigger on push to `master` branch with `paths: ['website/**']` filter, plus `workflow_dispatch`
    - Set permissions: `contents: read`, `pages: write`, `id-token: write`
    - Build job: checkout, setup Node 22, `npm ci`, `npm run build`, run tests (`npx vitest --run`), run Pagefind indexing, upload pages artifact
    - Deploy job: deploy to GitHub Pages
    - Ensure build failure prevents deployment
    - _Requirements: 11.1, 11.2, 11.3, 11.4, 15.4_

- [ ] 12. Checkpoint — Full build and deploy pipeline verification
  - Ensure `npm run build && npx pagefind --site dist && npx vitest --run` succeeds locally
  - Ensure all tests pass, ask the user if questions arise.

- [ ] 13. Write property-based tests against build output
  - [ ]* 13.1 Write property test for global navigation presence (Property 1)
    - **Property 1: Global navigation presence**
    - Parse every HTML file in `dist/`, verify each contains a `<nav>` with links to all six top-level sections
    - **Validates: Requirements 2.1**

  - [ ]* 13.2 Write property test for breadcrumb presence (Property 2)
    - **Property 2: Breadcrumb presence on non-landing pages**
    - Parse every non-index HTML file in `dist/`, verify breadcrumb nav element exists
    - **Validates: Requirements 2.2**

  - [ ]* 13.3 Write property test for documentation heading anchors (Property 3)
    - **Property 3: Documentation heading anchors**
    - Parse every docs HTML file, verify all `h1`–`h6` elements have `id` attributes
    - **Validates: Requirements 4.4**

  - [ ]* 13.4 Write property test for tutorial metadata display (Property 4)
    - **Property 4: Tutorial metadata display**
    - Parse every tutorial HTML file, verify reading time and prerequisites are displayed
    - **Validates: Requirements 5.3, 5.4**

  - [ ]* 13.5 Write property test for blog post display completeness (Property 5)
    - **Property 5: Blog post display completeness**
    - Verify blog index lists posts with title, date, author, summary in reverse chronological order
    - Verify each blog post page displays publication date and reading time
    - **Validates: Requirements 6.1, 6.4**

  - [ ]* 13.6 Write property test for Fifth syntax highlighting (Property 6)
    - **Property 6: Fifth syntax highlighting token differentiation**
    - Find all code blocks tagged as Fifth language, verify `<span>` elements with at least 3 distinct CSS classes for token categories
    - **Validates: Requirements 8.1**

  - [ ]* 13.7 Write property test for copy-to-clipboard on code blocks (Property 7)
    - **Property 7: Copy-to-clipboard on all code blocks**
    - Parse every page, find all `<pre><code>` blocks, verify each has a copy button element
    - **Validates: Requirements 3.4, 8.2**

  - [ ]* 13.8 Write property test for long code block line numbers (Property 8)
    - **Property 8: Long code block line numbers and scrolling**
    - Find all code blocks with more than 20 lines, verify line number elements and scrollable container
    - **Validates: Requirements 8.3**

  - [ ]* 13.9 Write property test for sitemap completeness (Property 10)
    - **Property 10: Sitemap completeness**
    - Parse `sitemap.xml`, compare against all HTML files in `dist/`, verify every public page has a `<url>` entry
    - **Validates: Requirements 13.1**

  - [ ]* 13.10 Write property test for meta tag presence (Property 11)
    - **Property 11: Meta tag presence**
    - Parse every HTML file, verify `<title>`, `<meta name="description">`, OG tags, and Twitter Card tags in `<head>`
    - **Validates: Requirements 13.2**

  - [ ]* 13.11 Write property test for semantic HTML structure (Property 12)
    - **Property 12: Semantic HTML structure**
    - Parse every HTML file, verify presence of `<header>`, `<nav>`, `<main>`, `<footer>`
    - **Validates: Requirements 13.3**

  - [ ]* 13.12 Write property test for clean URLs (Property 13)
    - **Property 13: Clean URLs**
    - Enumerate all page URLs from the build, verify none contain `.html` extensions or unnecessary path segments
    - **Validates: Requirements 13.4**

  - [ ]* 13.13 Write property test for internal link integrity (Property 14)
    - **Property 14: Internal link integrity**
    - Crawl all internal `<a href>` links across all pages, verify each resolves to an existing file or anchor in `dist/`
    - **Validates: Requirements 14.4**

  - [ ]* 13.14 Write property test for content readable without JavaScript (Property 15)
    - **Property 15: Content readable without JavaScript**
    - Parse every HTML file, verify `<main>` contains textual content in raw HTML (no JS required)
    - **Validates: Requirements 16.2**

  - [ ]* 13.15 Write property test for no external CDN dependencies (Property 16)
    - **Property 16: No external CDN dependencies for critical resources**
    - Parse every HTML file, verify no `<link rel="stylesheet">` or `<script>` tags reference external CDN domains
    - **Validates: Requirements 16.3**

- [ ] 14. Write unit and example tests
  - [ ]* 14.1 Write unit tests for landing page structure
    - Verify hero section, three feature cards, two CTAs, and code snippet exist in `dist/index.html`
    - _Requirements: 1.1, 1.2, 1.3, 1.4_

  - [ ]* 14.2 Write unit tests for specific page existence and content
    - Verify Getting Started pages exist (installation, first-program)
    - Verify documentation sections exist
    - Verify tutorial pages exist
    - Verify blog posts exist
    - _Requirements: 3.1, 4.1, 5.1, 6.2_

  - [ ]* 14.3 Write unit test for RSS feed validity
    - Verify `dist/blog/rss.xml` is valid XML with correct entries
    - _Requirements: 6.3_

  - [ ]* 14.4 Write unit test for 404 page
    - Verify `dist/404.html` exists with home link and search option
    - _Requirements: 16.4_

  - [ ]* 14.5 Write unit test for dark mode CSS tokens
    - Verify `global.css` output contains custom properties for both light and dark themes
    - _Requirements: 9.1, 9.4_

  - [ ]* 14.6 Write unit test for Fifth TextMate grammar validity
    - Verify `fifth.tmLanguage.json` is valid JSON with expected scope names
    - _Requirements: 8.4_

  - [ ]* 14.7 Write unit test for content migration correctness
    - Verify migrated content exists in output
    - Verify internal dev docs (Development, Planning, internal Designs) are NOT present in output
    - _Requirements: 14.1, 14.2, 14.3_

- [ ] 15. Final checkpoint — All tests pass, site is complete
  - Run `cd website && npm run build && npx pagefind --site dist && npx vitest --run`
  - Ensure all tests pass, ask the user if questions arise.

## Notes

- Tasks marked with `*` are optional and can be skipped for faster MVP
- Each task references specific requirements for traceability
- Checkpoints ensure incremental validation after each major phase
- Property tests validate universal correctness properties from the design document
- Unit tests validate specific examples and edge cases
- The existing `docs/` folder remains untouched — only selective content is copied to the website
- All code uses TypeScript within the Astro framework
