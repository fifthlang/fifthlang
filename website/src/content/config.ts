import { defineCollection, z } from 'astro:content';

const docs = defineCollection({
  type: 'content',
  schema: z.object({
    title: z.string(),
    description: z.string().optional(),
    order: z.number().optional(),
    category: z.string().optional(),
  }),
});

const blog = defineCollection({
  type: 'content',
  schema: z.object({
    title: z.string(),
    date: z.date(),
    author: z.string().default('Andrew Matthews'),
    summary: z.string(),
    draft: z.boolean().default(false),
  }),
});

const tutorials = defineCollection({
  type: 'content',
  schema: z.object({
    title: z.string(),
    description: z.string().optional(),
    readingTime: z.string(),
    prerequisites: z.array(z.string()).default([]),
    order: z.number().optional(),
    nextTutorial: z.string().optional(),
  }),
});

export const collections = { docs, blog, tutorials };
