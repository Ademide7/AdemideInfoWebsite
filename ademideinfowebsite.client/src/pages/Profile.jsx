import {
    Avatar,
    Box,
    Button,
    Card,
    CardContent,
    Divider,
    Stack,
    Typography,
} from "@mui/material";
import EditIcon from "@mui/icons-material/Edit";
import { Link } from "react-router-dom";
import useAuth from "../context/useAuth";

export default function Profile() {
    const { user } = useAuth();

    return (
        <Box maxWidth={700}>
            <Typography variant="h4" gutterBottom>
                My Profile
            </Typography>

            <Card>
                <CardContent>

                    <Stack
                        direction="column"
                        spacing={3}
                        alignItems="center"
                    >
                        <Avatar
                            sx={{
                                width: 90,
                                height: 90,
                                fontSize: 36,
                            }}
                        >
                            {user?.firstName?.charAt(0)}
                        </Avatar>

                        <Typography variant="h5">
                            {user?.firstName} {user?.lastName}
                        </Typography>

                        <Divider flexItem />

                        <Box width="100%">
                            <Typography variant="subtitle2">
                                First Name
                            </Typography>

                            <Typography mb={2}>
                                {user?.firstName}
                            </Typography>

                            <Typography variant="subtitle2">
                                Last Name
                            </Typography>

                            <Typography mb={2}>
                                {user?.lastName}
                            </Typography>

                            <Typography variant="subtitle2">
                                Email
                            </Typography>

                            <Typography mb={3}>
                                {user?.email}
                            </Typography>

                            <Button
                                component={Link}
                                to="/edit-profile"
                                variant="contained"
                                startIcon={<EditIcon />}
                            >
                                Edit Profile
                            </Button>
                        </Box>

                    </Stack>

                </CardContent>
            </Card>
        </Box>
    );
}