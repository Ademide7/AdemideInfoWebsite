import {
    AppBar,
    Toolbar,
    Typography,
    Avatar,
    Stack,
} from "@mui/material";

import useAuth from "../context/useAuth";

export default function Navbar() {
    const { user } = useAuth();

    return (
        <AppBar
            position="fixed"
            sx={{
                zIndex: (theme) => theme.zIndex.drawer + 1,
            }}
        >
            <Toolbar>

                <Typography
                    variant="h6"
                    sx={{ flexGrow: 1 }}
                >
                    Ademide Website
                </Typography>

                <Stack
                    direction="row"
                    spacing={2}
                    alignItems="center"
                >
                    <Typography>
                        {user?.firstName}
                    </Typography>

                    <Avatar>
                        {user?.firstName?.charAt(0)}
                    </Avatar>
                </Stack>

            </Toolbar>
        </AppBar>
    );
}