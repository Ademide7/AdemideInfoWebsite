import { useState } from "react";
import {
    Avatar,
    Box,
    Button,
    Card,
    CardContent,
    CircularProgress,
    Container,
    Divider,
    Grid,
    Stack,
    TextField,
    Typography,
    Alert,
} from "@mui/material";

import SaveIcon from "@mui/icons-material/Save";
import CameraAltIcon from "@mui/icons-material/CameraAlt";

import axios from "axios";
import useAuth from "../context/useAuth";

export default function EditProfile() {
    const { user, token, updateUser } = useAuth();

    const [loading, setLoading] = useState(false);

    const [success, setSuccess] = useState("");

    const [error, setError] = useState("");

    const [form, setForm] = useState({
        firstName: user?.firstName || "",
        lastName: user?.lastName || "",
        email: user?.email || "",
        phoneNumber: user?.phoneNumber || "",
        occupation: user?.occupation || "",
        location: user?.location || "",
        bio: user?.bio || "",
    });

    const handleChange = (e) => {
        setForm((prev) => ({
            ...prev,
            [e.target.name]: e.target.value,
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        setLoading(true);
        setError("");
        setSuccess("");

        try {
            const response = await axios.put(
                "https://localhost:7151/api/Profile/update",
                form,
                {
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                }
            );

            updateUser(response.data.data);

            setSuccess("Profile updated successfully.");
        } catch (err) {
            setError(
                err.response?.data?.message ||
                "Unable to update profile."
            );
        } finally {
            setLoading(false);
        }
    };

    return (
        <Container maxWidth="lg">
            <Typography
                variant="h4"
                fontWeight={700}
                gutterBottom
            >
                Edit Profile
            </Typography>

            <Typography
                color="text.secondary"
                mb={4}
            >
                Keep your personal information up to date.
            </Typography>

            <Card>
                <CardContent sx={{ p: 5 }}>
                    <Grid
                        container
                        spacing={5}
                    >
                        <Grid
                            item
                            xs={12}
                            md={4}
                        >
                            <Stack
                                spacing={3}
                                alignItems="center"
                            >
                                <Avatar
                                    sx={{
                                        width: 150,
                                        height: 150,
                                        fontSize: 60,
                                    }}
                                >
                                    {user?.firstName?.charAt(0)}
                                </Avatar>

                                <Button
                                    startIcon={<CameraAltIcon />}
                                    variant="outlined"
                                >
                                    Change Picture
                                </Button>

                                <Divider flexItem />

                                <Typography
                                    align="center"
                                    color="text.secondary"
                                >
                                    Upload a professional profile photo.
                                </Typography>
                            </Stack>
                        </Grid>

                        <Grid
                            item
                            xs={12}
                            md={8}
                        >
                            <Box
                                component="form"
                                onSubmit={handleSubmit}
                            >
                                {success && (
                                    <Alert
                                        severity="success"
                                        sx={{ mb: 2 }}
                                    >
                                        {success}
                                    </Alert>
                                )}

                                {error && (
                                    <Alert
                                        severity="error"
                                        sx={{ mb: 2 }}
                                    >
                                        {error}
                                    </Alert>
                                )}

                                <Grid
                                    container
                                    spacing={2}
                                >
                                    <Grid item xs={12} md={6}>
                                        <TextField
                                            label="First Name"
                                            name="firstName"
                                            value={form.firstName}
                                            onChange={handleChange}
                                        />
                                    </Grid>

                                    <Grid item xs={12} md={6}>
                                        <TextField
                                            label="Last Name"
                                            name="lastName"
                                            value={form.lastName}
                                            onChange={handleChange}
                                        />
                                    </Grid>

                                    <Grid item xs={12}>
                                        <TextField
                                            label="Email"
                                            name="email"
                                            value={form.email}
                                            onChange={handleChange}
                                        />
                                    </Grid>

                                    <Grid item xs={12} md={6}>
                                        <TextField
                                            label="Phone Number"
                                            name="phoneNumber"
                                            value={form.phoneNumber}
                                            onChange={handleChange}
                                        />
                                    </Grid>

                                    <Grid item xs={12} md={6}>
                                        <TextField
                                            label="Occupation"
                                            name="occupation"
                                            value={form.occupation}
                                            onChange={handleChange}
                                        />
                                    </Grid>

                                    <Grid item xs={12}>
                                        <TextField
                                            label="Location"
                                            name="location"
                                            value={form.location}
                                            onChange={handleChange}
                                        />
                                    </Grid>

                                    <Grid item xs={12}>
                                        <TextField
                                            label="Bio"
                                            name="bio"
                                            multiline
                                            rows={5}
                                            value={form.bio}
                                            onChange={handleChange}
                                        />
                                    </Grid>
                                </Grid>

                                <Button
                                    type="submit"
                                    variant="contained"
                                    size="large"
                                    startIcon={<SaveIcon />}
                                    disabled={loading}
                                    sx={{ mt: 4 }}
                                >
                                    {loading ? (
                                        <CircularProgress
                                            size={24}
                                            color="inherit"
                                        />
                                    ) : (
                                        "Save Changes"
                                    )}
                                </Button>
                            </Box>
                        </Grid>
                    </Grid>
                </CardContent>
            </Card>
        </Container>
    );
}