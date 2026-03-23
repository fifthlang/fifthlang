/**
 * rss.xml.ts — RSS feed endpoint for the blog.
 *
 * Uses @astrojs/rss to generate an RSS feed from the blog collection.
 * Includes title, date, summary, and link for each post.
 *
 * Validates: Requirements 6.3
 */

import rss from '@astrojs/rss';
import { getCollection } from 'astro:content';
import type { APIContext } from 'astro';

export async function GET(context: APIContext) {
  const posts = await getCollection('blog', ({ data }) => !data.draft);

  const sortedPosts = posts.sort(
    (a, b) => b.data.date.getTime() - a.data.date.getTime()
  );

  return rss({
    title: 'Fifth Language Blog',
    description:
      'Updates, design decisions, and release announcements from the Fifth language project.',
    site: context.site!.toString(),
    items: sortedPosts.map((post) => ({
      title: post.data.title,
      pubDate: post.data.date,
      description: post.data.summary,
      link: `/blog/${post.slug}/`,
    })),
  });
}
