import {
    Box,
    Grid,
    Card,
    CardContent,
    Typography,
    Button,
    Avatar,
    Stack,
} from "@mui/material";
import EventIcon from "@mui/icons-material/Event";
import PersonIcon from "@mui/icons-material/Person";
import DashboardIcon from "@mui/icons-material/Dashboard";
import { Link } from "react-router-dom";
import useAuth from "../Scripts/useAuth";

export default function Dashboard() {
    const { user } = useAuth();

    return (
        <Box>
            <Typography variant="h4" fontWeight={600} gutterBottom>
                Dashboard
            </Typography>

            <Typography color="text.secondary" mb={4}>
                Welcome back, {user?.firstName}
            </Typography>

            <Grid container spacing={3}>
                <Grid item xs={12} md={4}>
                    <Card>
                        <CardContent>
                            <Stack spacing={2} alignItems="center">
                                <Avatar sx={{ width: 70, height: 70 }}>
                                    {user?.firstName?.charAt(0)}
                                </Avatar>

                                <Typography variant="h6">
                                    {user?.firstName} {user?.lastName}
                                </Typography>

                                <Typography color="text.secondary">
                                    {user?.email}
                                </Typography>

                                <Button
                                    component={Link}
                                    to="/profile"
                                    variant="contained"
                                    startIcon={<PersonIcon />}
                                >
                                    View Profile
                                </Button>
                            </Stack>
                        </CardContent>
                    </Card>
                </Grid>

                <Grid item xs={12} md={4}>
                    <Card sx={{ height: "100%" }}>
                        <CardContent>
                            <DashboardIcon color="primary" fontSize="large" />

                            <Typography variant="h6" mt={2}>
                                Dashboard
                            </Typography>

                            <Typography color="text.secondary">
                                Manage your profile and appointments from one place.
                            </Typography>
                        </CardContent>
                    </Card>
                </Grid>

                <Grid item xs={12} md={4}>
                    <Card sx={{ height: "100%" }}>
                        <CardContent>
                            <EventIcon color="primary" fontSize="large" />

                            <Typography variant="h6" mt={2}>
                                Appointments
                            </Typography>

                            <Typography color="text.secondary" mb={2}>
                                View and create appointments.
                            </Typography>

                            <Button
                                component={Link}
                                to="/appointments"
                                variant="contained"
                            >
                                Open
                            </Button>
                        </CardContent>
                    </Card>
                </Grid>
            </Grid>
        </Box>
    );
}