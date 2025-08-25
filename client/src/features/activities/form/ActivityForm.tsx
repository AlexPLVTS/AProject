
import { Box, Button, Paper, Typography } from "@mui/material"
import { useActivities } from "../../../lib/hooks/useActivities"
import { useNavigate, useParams } from "react-router";
import { useForm } from 'react-hook-form';
import { useEffect } from "react";
import { zodResolver } from '@hookform/resolvers/zod';
import { activitySchema, type ActivitySchema } from "../../../lib/schemas/activitySchema";
import TextInput from "../../../app/shared/components/TextInput";
import { categoryOptions } from "./categoryOptions";
import SelectInput from "../../../app/shared/components/SelectInput";
import DateTimeInput from "../../../app/shared/components/DateTimeInput";

export default function ActivityForm() {

    const { control, reset, handleSubmit } = useForm<ActivitySchema>({
        mode: 'onTouched',
        resolver: zodResolver(activitySchema),
        defaultValues: {
            title: '',
            description: '',
            category: '',
            date: new Date(),
            city: '',
            venue: ''
        }
    });

    const navigate = useNavigate();

    const {id} = useParams();

    const {updateActivity, createActivity, activity, isLoadingActivity} = useActivities(id);

    useEffect(() => {
        if (activity) reset({
            ...activity
        })
    }, [activity, reset]);

    const onSubmit = async (data: ActivitySchema) => {
/*        const { ...activity } = data;*/
        try {
            if (activity) {
                updateActivity.mutate({ ...data, id: activity.id }, {
                    onSuccess: () => {
                        navigate(`/activities/${activity.id}`);
                    }
                });
            } else {
                createActivity.mutate(data, {
                    onSuccess: (newActivityId) => {
                        navigate(`/activities/${newActivityId}`);
                    }
                });
            }
        } catch (error) {
            console.log(error)
        }
    }

    if (isLoadingActivity) return <Typography>Loading activity...</Typography>

    return (
        <Paper sx={{borderRadius: 3, padding: 3}}>
            <Typography variant="h5" gutterBottom color="primary">
                {activity ? 'Edit activity' : 'Create activity'}
            </Typography>
            <Box component='form' onSubmit={handleSubmit(onSubmit)} display='flex' flexDirection='column' gap={3}>
                <TextInput label='Title' control={control} name='title' />
                <TextInput label='Description' control={control} name='description' />
                <Box display='flex' gap={3}>
                <SelectInput
                    items={categoryOptions}
                    label='Category'
                    control={control}
                    name='category' />
                    <DateTimeInput label='Date' control={control} name='date' />
                </Box>
                <TextInput label='City' control={control} name='city' />
                <TextInput label='Venue' control={control} name='venue' />
                <Box display='flex' justifyContent='end' gap={3}>
                    <Button color='inherit'>Cancel</Button>
                    <Button
                        type="submit"
                        color='success'
                        variant="contained"
                        disabled={updateActivity.isPending || createActivity.isPending}
                    >Submit</Button>
                </Box>
            </Box>
        </Paper>
    )
}