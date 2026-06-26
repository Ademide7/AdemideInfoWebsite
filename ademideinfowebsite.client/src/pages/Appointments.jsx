import { useState } from "react";
import {
    Box,
    Button,
    Card,
    CardContent,
    Chip,
    Stack,
    Typography,
} from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import { Link } from "react-router-dom";

export default function Appointments() {

    const [appointments] = useState([
        {
            id: 1,
            title: "Project Meeting",
            date: "26 June 2026",
            status: "Pending",
        },
        {
            id: 2,
            title: "Client Interview",
            date: "30 June 2026",
            status: "Completed",
        },
    ]);

    return (
        <Box>

            <Stack
                direction="row"
                justifyContent="space-between"
                mb={3}
            >
                <Typography variant="h4">
                    Appointments
                </Typography>

                <Button
                    variant="contained"
                    startIcon={<AddIcon />}
                    component={Link}
                    to="/appointments/create"
                >
                    New Appointment
                </Button>
            </Stack>

            <Stack spacing={2}>

                {appointments.map((appointment) => (
                    <Card key={appointment.id}>
                        <CardContent>

                            <Stack
                                direction="row"
                                justifyContent="space-between"
                                alignItems="center"
                            >
                                <Box>

                                    <Typography variant="h6">
                                        {appointment.title}
                                    </Typography>

                                    <Typography color="text.secondary">
                                        {appointment.date}
                                    </Typography>

                                </Box>

                                <Chip
                                    color={
                                        appointment.status === "Completed"
                                            ? "success"
                                            : "warning"
                                    }
                                    label={appointment.status}
                                />

                            </Stack>

                        </CardContent>
                    </Card>
                ))}

            </Stack>

        </Box>
    );
}