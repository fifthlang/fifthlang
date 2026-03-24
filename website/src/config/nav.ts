/**
 * Navigation configuration for the Fifth language website.
 *
 * docCategories  – defines sidebar ordering for the documentation section.
 * navItems       – defines the persistent top-level navigation links.
 */

export const docCategories = [
  { slug: 'getting-started', label: 'Getting Started' },
  { slug: 'language-reference', label: 'Language Reference' },
  { slug: 'knowledge-graphs', label: 'Knowledge Graphs' },
  { slug: 'sdk-reference', label: 'SDK Reference' },
  { slug: 'compiler-cli', label: 'Compiler CLI' },
];

export const navItems = [
  { label: 'Home', href: '/' },
  { label: 'Getting Started', href: '/docs/getting-started/installation' },
  { label: 'Documentation', href: '/docs' },
  { label: 'Tutorials', href: '/tutorials' },
  { label: 'Blog', href: '/blog' },
  { label: 'Get Involved', href: '/get-involved' },
];
