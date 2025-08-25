import { Link, SearchOff } from "@mui/icons-material";
import { Button, Paper, Typography } from "@mui/material";
import MenuItemLink from "../../app/shared/components/MenuItemLink";

export default function NotFound() {
    return (
        <Paper
            sx={{
                height: 400,
                display: 'flex',
                flexDirection: 'column',
                justifyContent: 'center',
                alignItems: 'center',
                p: 6
            }}
        >
            <SearchOff sx={{ fontSize: 100 }} color='primary' />
            <Typography gutterBottom variant='h3'>
                Oops - we could not found what you are looking for
            </Typography>
            <MenuItemLink to='/activities'>
                Return
            </MenuItemLink>
        </Paper>
    )
}