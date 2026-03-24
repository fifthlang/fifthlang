import { defineConfig } from 'astro/config';
import mdx from '@astrojs/mdx';
import sitemap from '@astrojs/sitemap';
import rehypeSlug from 'rehype-slug';
import rehypeAutolinkHeadings from 'rehype-autolink-headings';
import fs from 'node:fs';

// Load custom Fifth TextMate grammar if available (created in task 1.3)
let fifthLangs = [];
try {
  const grammar = JSON.parse(fs.readFileSync('./fifth.tmLanguage.json', 'utf-8'));
  fifthLangs = [grammar];
} catch {
  // Grammar file doesn't exist yet — Shiki will skip Fifth highlighting
}

export default defineConfig({
  site: 'https://fifthlang.org',
  outDir: './dist',
  integrations: [mdx(), sitemap()],
  markdown: {
    rehypePlugins: [rehypeSlug, rehypeAutolinkHeadings],
    shikiConfig: {
      themes: {
        light: 'github-light',
        dark: 'github-dark',
      },
      langs: fifthLangs,
    },
  },
});
