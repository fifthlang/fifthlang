// Feature: fifthlang-website — Unit and example tests (Tasks 14.1–14.7)
// Tests validate specific page structure, content existence, and configuration correctness.

import { describe, it, expect } from 'vitest';
import * as cheerio from 'cheerio';
import fs from 'node:fs';
import path from 'node:path';

// ---------------------------------------------------------------------------
// Helpers
// ---------------------------------------------------------------------------

const DIST_DIR = path.resolve(__dirname, '../dist');

/** Load and parse an HTML file with cheerio. */
function loadHtml(filePath: string) {
  const html = fs.readFileSync(filePath, 'utf-8');
  return cheerio.load(html);
}

// ---------------------------------------------------------------------------
// 14.1 — Landing page structure
// Validates: Requirements 1.1, 1.2, 1.3, 1.4
// ---------------------------------------------------------------------------

describe('14.1: Landing page structure', () => {
  const indexPath = path.join(DIST_DIR, 'index.html');
  const $ = loadHtml(indexPath);

  it('has a hero section', () => {
    const hero = $('section.hero, [aria-label*="Introduction"]');
    expect(hero.length).toBeGreaterThanOrEqual(1);
  });

  it('has three feature cards', () => {
    const cards = $('.feature-card');
    expect(cards.length).toBe(3);
  });

  it('has two CTAs: "Install Fifth" and "Learn in Y Minutes"', () => {
    const ctas = $('a.cta');
    const ctaTexts = ctas.map((_, el) => $(el).text().trim()).get();
    expect(ctaTexts).toContain('Install Fifth');
    expect(ctaTexts).toContain('Learn in Y Minutes');
  });

  it('has a code snippet in the hero section', () => {
    const heroCode = $('section.hero .code-block-wrapper');
    expect(heroCode.length).toBeGreaterThanOrEqual(1);
    // Verify it contains Fifth code
    const codeText = heroCode.find('code').text();
    expect(codeText).toContain('graph');
  });
});

// ---------------------------------------------------------------------------
// 14.2 — Specific page existence and content
// Validates: Requirements 3.1, 4.1, 5.1, 6.2
// ---------------------------------------------------------------------------

describe('14.2: Specific page existence and content', () => {
  it('Getting Started pages exist (installation, first-program)', () => {
    expect(fs.existsSync(path.join(DIST_DIR, 'docs/getting-started/installation/index.html'))).toBe(true);
    expect(fs.existsSync(path.join(DIST_DIR, 'docs/getting-started/first-program/index.html'))).toBe(true);
  });

  it('documentation sections exist (sdk-reference, language-reference, compiler-cli)', () => {
    expect(fs.existsSync(path.join(DIST_DIR, 'docs/sdk-reference/index.html'))).toBe(true);
    expect(fs.existsSync(path.join(DIST_DIR, 'docs/language-reference/index.html'))).toBe(true);
    expect(fs.existsSync(path.join(DIST_DIR, 'docs/compiler-cli/index.html'))).toBe(true);
  });

  it('tutorial pages exist (learn-fifth-in-y-minutes, working-with-knowledge-graphs)', () => {
    expect(fs.existsSync(path.join(DIST_DIR, 'tutorials/learn-fifth-in-y-minutes/index.html'))).toBe(true);
    expect(fs.existsSync(path.join(DIST_DIR, 'tutorials/working-with-knowledge-graphs/index.html'))).toBe(true);
  });

  it('all 4 blog posts exist', () => {
    const expectedPosts = [
      'blog/2025-11-16-announcing-fifth/index.html',
      'blog/2025-11-28-release-packaging-pipeline/index.html',
      'blog/2025-12-05-the-story-of-fifth/index.html',
      'blog/2025-09-16-graph-assertion-block/index.html',
    ];
    for (const post of expectedPosts) {
      expect(fs.existsSync(path.join(DIST_DIR, post)), `${post} should exist`).toBe(true);
    }
  });
});

// ---------------------------------------------------------------------------
// 14.3 — RSS feed validity
// Validates: Requirements 6.3
// ---------------------------------------------------------------------------

describe('14.3: RSS feed validity', () => {
  const rssPath = path.join(DIST_DIR, 'blog/rss.xml');

  it('rss.xml exists', () => {
    expect(fs.existsSync(rssPath)).toBe(true);
  });

  it('is valid XML with correct RSS structure', () => {
    const xml = fs.readFileSync(rssPath, 'utf-8');
    const $ = cheerio.load(xml, { xmlMode: true });

    // Root element is <rss>
    expect($('rss').length).toBe(1);
    expect($('rss').attr('version')).toBe('2.0');

    // Has a <channel> with title, description, link
    expect($('channel > title').text()).toBeTruthy();
    expect($('channel > description').text()).toBeTruthy();
    expect($('channel > link').text()).toBeTruthy();
  });

  it('contains entries with title, link, and description for each post', () => {
    const xml = fs.readFileSync(rssPath, 'utf-8');
    const $ = cheerio.load(xml, { xmlMode: true });

    const items = $('item');
    expect(items.length).toBe(4);

    items.each((_, el) => {
      const $item = $(el);
      expect($item.find('title').text(), 'item should have title').toBeTruthy();
      expect($item.find('link').text(), 'item should have link').toBeTruthy();
      expect($item.find('description').text(), 'item should have description').toBeTruthy();
    });
  });
});

// ---------------------------------------------------------------------------
// 14.4 — 404 page
// Validates: Requirements 16.4
// ---------------------------------------------------------------------------

describe('14.4: 404 page', () => {
  const notFoundPath = path.join(DIST_DIR, '404.html');

  it('404.html exists', () => {
    expect(fs.existsSync(notFoundPath)).toBe(true);
  });

  it('has a home link', () => {
    const $ = loadHtml(notFoundPath);
    const homeLink = $('a[href="/"]');
    expect(homeLink.length).toBeGreaterThanOrEqual(1);
  });

  it('has a search hint', () => {
    const $ = loadHtml(notFoundPath);
    const searchHint = $('.search-hint');
    expect(searchHint.length).toBeGreaterThanOrEqual(1);
    expect(searchHint.text()).toMatch(/search/i);
  });
});

// ---------------------------------------------------------------------------
// 14.5 — Dark mode CSS tokens
// Validates: Requirements 9.1, 9.4
// ---------------------------------------------------------------------------

describe('14.5: Dark mode CSS tokens', () => {
  const cssPath = path.resolve(__dirname, '../src/styles/global.css');
  const css = fs.readFileSync(cssPath, 'utf-8');

  it('defines custom properties in :root for light theme', () => {
    expect(css).toMatch(/:root\s*\{/);
    expect(css).toContain('--color-accent-magenta');
    expect(css).toContain('--color-accent-orange');
    expect(css).toContain('--color-bg-primary');
    expect(css).toContain('--color-text-primary');
    expect(css).toContain('--font-sans');
    expect(css).toContain('--font-mono');
    expect(css).toContain('--space-4');
  });

  it('defines [data-theme="dark"] selector with overrides', () => {
    expect(css).toMatch(/\[data-theme="dark"\]\s*\{/);
    // Dark theme should override background and text colors
    expect(css).toMatch(/\[data-theme="dark"\][^}]*--color-bg-primary/s);
    expect(css).toMatch(/\[data-theme="dark"\][^}]*--color-text-primary/s);
  });
});

// ---------------------------------------------------------------------------
// 14.6 — Fifth TextMate grammar validity
// Validates: Requirements 8.4
// ---------------------------------------------------------------------------

describe('14.6: Fifth TextMate grammar validity', () => {
  const grammarPath = path.resolve(__dirname, '../fifth.tmLanguage.json');

  it('is valid JSON', () => {
    const raw = fs.readFileSync(grammarPath, 'utf-8');
    expect(() => JSON.parse(raw)).not.toThrow();
  });

  it('has expected scope names', () => {
    const grammar = JSON.parse(fs.readFileSync(grammarPath, 'utf-8'));

    expect(grammar.scopeName).toBe('source.fifth');

    // Flatten all scope names from the grammar repository
    const allText = JSON.stringify(grammar);

    const expectedScopes = [
      'keyword.control.fifth',
      'entity.name.type.fifth',
      'string.quoted.double.fifth',
      'comment.line.double-slash.fifth',
    ];

    for (const scope of expectedScopes) {
      expect(allText, `grammar should contain scope "${scope}"`).toContain(scope);
    }
  });
});

// ---------------------------------------------------------------------------
// 14.7 — Content migration correctness
// Validates: Requirements 14.1, 14.2, 14.3
// ---------------------------------------------------------------------------

describe('14.7: Content migration correctness', () => {
  it('migrated content exists in output', () => {
    // Getting Started docs
    expect(fs.existsSync(path.join(DIST_DIR, 'docs/getting-started/installation/index.html'))).toBe(true);

    // Tutorials
    expect(fs.existsSync(path.join(DIST_DIR, 'tutorials/learn-fifth-in-y-minutes/index.html'))).toBe(true);
    expect(fs.existsSync(path.join(DIST_DIR, 'tutorials/working-with-knowledge-graphs/index.html'))).toBe(true);

    // Blog posts
    expect(fs.existsSync(path.join(DIST_DIR, 'blog/2025-11-16-announcing-fifth/index.html'))).toBe(true);
    expect(fs.existsSync(path.join(DIST_DIR, 'blog/2025-12-05-the-story-of-fifth/index.html'))).toBe(true);

    // SDK reference
    expect(fs.existsSync(path.join(DIST_DIR, 'docs/sdk-reference/index.html'))).toBe(true);
  });

  it('internal dev docs are NOT present in output', () => {
    // Development docs should not be in the public site
    expect(fs.existsSync(path.join(DIST_DIR, 'Development'))).toBe(false);
    expect(fs.existsSync(path.join(DIST_DIR, 'development'))).toBe(false);

    // Planning docs should not be in the public site
    expect(fs.existsSync(path.join(DIST_DIR, 'Planning'))).toBe(false);
    expect(fs.existsSync(path.join(DIST_DIR, 'planning'))).toBe(false);

    // Internal Designs (except SDK reference which was migrated) should not be present
    expect(fs.existsSync(path.join(DIST_DIR, 'Designs'))).toBe(false);
    expect(fs.existsSync(path.join(DIST_DIR, 'designs'))).toBe(false);
  });
});
