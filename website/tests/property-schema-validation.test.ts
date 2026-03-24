// Feature: fifthlang-website, Property 9: Content collection schema validation
// **Validates: Requirements 12.1, 12.2, 12.3**

import { describe, it, expect } from 'vitest';
import * as fc from 'fast-check';
import { z } from 'zod';

// Re-create the same Zod schemas from src/content/config.ts
// (since we can't import from astro:content in tests)

const docsSchema = z.object({
  title: z.string(),
  description: z.string().optional(),
  order: z.number().optional(),
  category: z.string().optional(),
});

const blogSchema = z.object({
  title: z.string(),
  date: z.date(),
  author: z.string().default('Andrew Matthews'),
  summary: z.string(),
  draft: z.boolean().default(false),
});

const tutorialsSchema = z.object({
  title: z.string(),
  description: z.string().optional(),
  readingTime: z.string(),
  prerequisites: z.array(z.string()).default([]),
  order: z.number().optional(),
  nextTutorial: z.string().optional(),
});

// --- Arbitraries for valid frontmatter ---

const nonEmptyString = fc.string({ minLength: 1, maxLength: 200 });

const validDocsArb = fc.record({
  title: nonEmptyString,
  description: fc.option(nonEmptyString, { nil: undefined }),
  order: fc.option(fc.integer({ min: 0, max: 1000 }), { nil: undefined }),
  category: fc.option(nonEmptyString, { nil: undefined }),
});

const validDateArb = fc.date({ min: new Date('2020-01-01'), max: new Date('2030-12-31') })
  .filter((d) => !isNaN(d.getTime()));

const validBlogArb = fc.record({
  title: nonEmptyString,
  date: validDateArb,
  author: fc.option(nonEmptyString, { nil: undefined }),
  summary: nonEmptyString,
  draft: fc.option(fc.boolean(), { nil: undefined }),
});

const validTutorialsArb = fc.record({
  title: nonEmptyString,
  description: fc.option(nonEmptyString, { nil: undefined }),
  readingTime: nonEmptyString,
  prerequisites: fc.option(fc.array(nonEmptyString, { maxLength: 10 }), { nil: undefined }),
  order: fc.option(fc.integer({ min: 0, max: 100 }), { nil: undefined }),
  nextTutorial: fc.option(nonEmptyString, { nil: undefined }),
});


describe('Property 9: Content collection schema validation', () => {
  describe('docs schema accepts valid frontmatter', () => {
    it('should accept any valid docs frontmatter', () => {
      fc.assert(
        fc.property(validDocsArb, (frontmatter) => {
          const result = docsSchema.safeParse(frontmatter);
          expect(result.success).toBe(true);
        }),
        { numRuns: 100 },
      );
    });
  });

  describe('blog schema accepts valid frontmatter', () => {
    it('should accept any valid blog frontmatter', () => {
      fc.assert(
        fc.property(validBlogArb, (frontmatter) => {
          const result = blogSchema.safeParse(frontmatter);
          expect(result.success).toBe(true);
        }),
        { numRuns: 100 },
      );
    });
  });

  describe('tutorials schema accepts valid frontmatter', () => {
    it('should accept any valid tutorials frontmatter', () => {
      fc.assert(
        fc.property(validTutorialsArb, (frontmatter) => {
          const result = tutorialsSchema.safeParse(frontmatter);
          expect(result.success).toBe(true);
        }),
        { numRuns: 100 },
      );
    });
  });

  describe('docs schema rejects invalid frontmatter', () => {
    it('should reject docs frontmatter with missing title', () => {
      fc.assert(
        fc.property(
          fc.record({
            description: fc.option(nonEmptyString, { nil: undefined }),
            order: fc.option(fc.integer(), { nil: undefined }),
            category: fc.option(nonEmptyString, { nil: undefined }),
          }),
          (frontmatter) => {
            const result = docsSchema.safeParse(frontmatter);
            expect(result.success).toBe(false);
          },
        ),
        { numRuns: 100 },
      );
    });

    it('should reject docs frontmatter with wrong title type', () => {
      fc.assert(
        fc.property(
          fc.record({
            title: fc.oneof(fc.integer(), fc.boolean(), fc.constant(null)),
            description: fc.option(nonEmptyString, { nil: undefined }),
          }),
          (frontmatter) => {
            const result = docsSchema.safeParse(frontmatter);
            expect(result.success).toBe(false);
          },
        ),
        { numRuns: 100 },
      );
    });

    it('should reject docs frontmatter with wrong order type', () => {
      fc.assert(
        fc.property(
          fc.record({
            title: nonEmptyString,
            order: fc.oneof(nonEmptyString, fc.boolean()),
          }),
          (frontmatter) => {
            const result = docsSchema.safeParse(frontmatter);
            expect(result.success).toBe(false);
          },
        ),
        { numRuns: 100 },
      );
    });
  });

  describe('blog schema rejects invalid frontmatter', () => {
    it('should reject blog frontmatter with missing required fields', () => {
      fc.assert(
        fc.property(
          fc.record({
            title: nonEmptyString,
            // missing date and summary
          }),
          (frontmatter) => {
            const result = blogSchema.safeParse(frontmatter);
            expect(result.success).toBe(false);
          },
        ),
        { numRuns: 100 },
      );
    });

    it('should reject blog frontmatter with wrong date type', () => {
      fc.assert(
        fc.property(
          fc.record({
            title: nonEmptyString,
            date: nonEmptyString, // string instead of Date
            summary: nonEmptyString,
          }),
          (frontmatter) => {
            const result = blogSchema.safeParse(frontmatter);
            expect(result.success).toBe(false);
          },
        ),
        { numRuns: 100 },
      );
    });

    it('should reject blog frontmatter with wrong summary type', () => {
      fc.assert(
        fc.property(
          fc.record({
            title: nonEmptyString,
            date: validDateArb,
            summary: fc.oneof(fc.integer(), fc.boolean(), fc.constant(null)),
          }),
          (frontmatter) => {
            const result = blogSchema.safeParse(frontmatter);
            expect(result.success).toBe(false);
          },
        ),
        { numRuns: 100 },
      );
    });
  });

  describe('tutorials schema rejects invalid frontmatter', () => {
    it('should reject tutorials frontmatter with missing readingTime', () => {
      fc.assert(
        fc.property(
          fc.record({
            title: nonEmptyString,
            // missing readingTime
          }),
          (frontmatter) => {
            const result = tutorialsSchema.safeParse(frontmatter);
            expect(result.success).toBe(false);
          },
        ),
        { numRuns: 100 },
      );
    });

    it('should reject tutorials frontmatter with wrong readingTime type', () => {
      fc.assert(
        fc.property(
          fc.record({
            title: nonEmptyString,
            readingTime: fc.oneof(fc.integer(), fc.boolean(), fc.constant(null)),
          }),
          (frontmatter) => {
            const result = tutorialsSchema.safeParse(frontmatter);
            expect(result.success).toBe(false);
          },
        ),
        { numRuns: 100 },
      );
    });

    it('should reject tutorials frontmatter with wrong prerequisites type', () => {
      fc.assert(
        fc.property(
          fc.record({
            title: nonEmptyString,
            readingTime: nonEmptyString,
            prerequisites: fc.oneof(
              nonEmptyString, // string instead of array
              fc.integer(),
            ),
          }),
          (frontmatter) => {
            const result = tutorialsSchema.safeParse(frontmatter);
            expect(result.success).toBe(false);
          },
        ),
        { numRuns: 100 },
      );
    });
  });
});
