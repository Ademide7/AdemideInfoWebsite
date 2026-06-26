import { useState } from "react";
import {
    Box,
    Button,
    Container,
    MenuItem,
    Paper,
    TextField,
    Typography,
} from "@mui/material";
import { Link, useNavigate } from "react-router-dom";
import axios from "axios";

const API_URL = "https://localhost:7151/api/Profile/create";

export default function Register() {
    const navigate = useNavigate();

    const [loading, setLoading] = useState(false);

    const [form, setForm] = useState({
        firstName: "",
        lastName: "",
        email: "",
        password: "",
        language: 0,
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

        try {
            const response = await axios.post(API_URL, form);

            const result = response.data;

            if (result.status) {
                localStorage.setItem("token", result.data.token);
                localStorage.setItem(
                    "user",
                    JSON.stringify(result.data.profileDetails)
                );

                navigate("/dashboard");
            } else {
                alert(result.message);
            }
        } catch (err) {
            console.log(err);

            alert(
                err.response?.data?.message ||
                "Registration failed."
            );
        } finally {
            setLoading(false);
        }
    };

    return (
        <Container maxWidth="sm">
            <Box
                sx={{
                    minHeight: "100vh",
                    display: "flex",
                    alignItems: "center",
                }}
            >
                <Paper
                    elevation={4}
                    sx={{
                        p: 4,
                        width: "100%",
                        borderRadius: 3,
                    }}
                >
                    <Typography
                        variant="h4"
                        align="center"
                        gutterBottom
                    >
                        Create Account
                    </Typography>

                    <Typography
                        align="center"
                        color="text.secondary"
                        mb={3}
                    >
                        Register to manage your appointments.
                    </Typography>

                    <Box
                        component="form"
                        onSubmit={handleSubmit}
                    >
                        <TextField
                            label="First Name"
                            name="firstName"
                            value={form.firstName}
                            onChange={handleChange}
                            required
                        />

                        <TextField
                            label="Last Name"
                            name="lastName"
                            value={form.lastName}
                            onChange={handleChange}
                            required
                        />

                        <TextField
                            label="Email"
                            name="email"
                            type="email"
                            value={form.email}
                            onChange={handleChange}
                            required
                        />

                        <TextField
                            label="Password"
                            name="password"
                            type="password"
                            value={form.password}
                            onChange={handleChange}
                            required
                        />

                        <TextField
                            select
                            label="Language"
                            name="language"
                            value={form.language}
                            onChange={handleChange}
                        >
                            <MenuItem value={0}>
                                English
                            </MenuItem>

                            <MenuItem value={1}>
                                French
                            </MenuItem>

                            <MenuItem value={2}>
                                Spanish
                            </MenuItem>
                        </TextField>

                        <Button
                            type="submit"
                            variant="contained"
                            fullWidth
                            size="large"
                            disabled={loading}
                            sx={{ mt: 2 }}
                        >
                            {loading ? "Creating Account..." : "Register"}
                        </Button>

                        <Button
                            component={Link}
                            to="/login"
                            fullWidth
                            sx={{ mt: 2 }}
                        >
                            Already have an account? Login
                        </Button>
                    </Box>
                </Paper>
            </Box>
        </Container>
    );
}