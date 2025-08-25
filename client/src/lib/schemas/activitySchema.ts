import { z } from 'zod';
import { requiredString } from '../util/util';


export const activitySchema = z.object({
    title: requiredString('Title'),
    description: requiredString('Description'),
    category: requiredString('Category'),
    date: z.coerce.date({
        message: 'Date is required'
    }),
    city: requiredString('City'),
    venue: requiredString('Venue'),
})

export type ActivitySchema = z.infer<typeof activitySchema>;