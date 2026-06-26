 import {
    Box,
    Button,
    Card,
    CardContent,
    Chip,
    Container,
    Grid,
    Stack,
    Typography,
} from "@mui/material";
import {
    Code,
    Cloud,
    Storage,
    PhoneAndroid,
    Work,
} from "@mui/icons-material";
import { Link } from "react-router-dom";

export default function Home() {
    const skills = [".NET 10", "C#", "ASP.NET Core", "React", "JavaScript", "SQL Server", "Azure", "Docker", "Flutter", "Git"];

    const services = [
        { icon: <Code color="primary" sx={{ fontSize: 48 }} />, title: "Web Development", desc: "Modern enterprise applications with ASP.NET Core & React." },
        { icon: <Cloud color="primary" sx={{ fontSize: 48 }} />, title: "Cloud Solutions", desc: "Azure cloud, APIs and scalable backend services." },
        { icon: <Storage color="primary" sx={{ fontSize: 48 }} />, title: "Database Design", desc: "SQL Server, PostgreSQL and high-performance data solutions." },
    ];

    const projects = [
        { icon: <Work color="primary" />, title: "Portfolio Website", desc: "Modern portfolio built with React & ASP.NET Core." },
        { icon: <PhoneAndroid color="primary" />, title: "Savings App", desc: "Flutter application with secure REST APIs." },
        { icon: <Cloud color="primary" />, title: "Enterprise API", desc: "JWT secured .NET APIs with SQL Server." },
    ];

    return (
        <>
            <Box
                sx={{
                    minHeight: "90vh",
                    display: "flex",
                    alignItems: "center",
                    background:
                        "linear-gradient(135deg,#eef4ff 0%,#f8faff 50%,#ffffff 100%)",
                }}
            >
                <Container maxWidth="lg">
                    <Grid container spacing={6} alignItems="center">
                        <Grid size={{ xs: 12, md: 7 }}>
                            <Chip
                                label="Available for Projects"
                                color="success"
                                sx={{ mb: 3, fontWeight: 600 }}
                            />

                            <Typography variant="h2" fontWeight={800} color="text.danger">
                                Ibrahim Ademide
                            </Typography>

                            <Typography
                                variant="h4"
                                color="primary"
                                sx={{ mt: 2, fontWeight: 700 }}
                            >
                                Full Stack .NET Engineer
                            </Typography>

                            <Typography
                                sx={{
                                    mt: 3,
                                    color: "text.secondary",
                                    fontSize: 18,
                                    maxWidth: 600,
                                }}
                            >
                                I build secure, scalable and modern web, cloud and mobile
                                applications using ASP.NET Core, React, Azure, SQL Server and
                                Flutter.
                            </Typography>

                            <Stack direction="row" spacing={2} mt={5}>
                                <Button
                                    component={Link}
                                    to="/register"
                                    variant="contained"
                                    size="large"
                                >
                                    Hire Me
                                </Button>

                                <Button
                                    component={Link}
                                    to="/login"
                                    variant="outlined"
                                    size="large"
                                >
                                    Client Login
                                </Button>
                            </Stack>

                            <Stack direction="row" spacing={6} mt={6}>
                                <Box>
                                    <Typography variant="h4" color="primary" fontWeight={700}>
                                        6+
                                    </Typography>
                                    <Typography color="text.secondary">Years</Typography>
                                </Box>

                                <Box>
                                    <Typography variant="h4" color="primary" fontWeight={700}>
                                        100%
                                    </Typography>
                                    <Typography color="text.secondary">Commitment</Typography>
                                </Box>
                            </Stack>
                        </Grid>

                        <Grid size={{ xs: 12, md: 5 }}>
                            <Card
                                sx={{
                                    p: 4,
                                    borderRadius: 5,
                                    textAlign: "center",
                                }}
                            >
                                <Typography variant="h1">👨‍💻</Typography>

                                <Typography variant="h5" mt={2}>
                                    Building Software That Matters
                                </Typography>

                                <Typography color="text.secondary" mt={1}>
                                    .NET • React • Azure • Flutter
                                </Typography>

                                <Stack
                                    direction="row"
                                    spacing={1}
                                    useFlexGap
                                    flexWrap="wrap"
                                    justifyContent="center"
                                    mt={3}
                                >
                                    {[".NET", "React", "Azure", "SQL"].map(x => (
                                        <Chip key={x} label={x} />
                                    ))}
                                </Stack>
                            </Card>
                        </Grid>
                    </Grid>
                </Container>
            </Box>

            <Container maxWidth="lg" sx={{ py: 10 }}>
                <Typography variant="h3" align="center" fontWeight={700} gutterBottom>
                    About Me
                </Typography>

                <Typography
                    align="center"
                    color="text.secondary"
                    sx={{ maxWidth: 850, mx: "auto", mt: 2 }}
                >
                    Passionate Full Stack Software Engineer specializing in enterprise
                    applications, clean architecture, cloud solutions and modern user
                    experiences. I enjoy transforming business ideas into reliable
                    software products.
                </Typography>
            </Container>

            <Container maxWidth="lg" sx={{ pb: 10 }}>
                <Typography variant="h3" align="center" fontWeight={700}>
                    Technologies
                </Typography>

                <Stack
                    direction="row"
                    spacing={2}
                    flexWrap="wrap"
                    useFlexGap
                    justifyContent="center"
                    mt={5}
                >
                    {skills.map((skill) => (
                        <Chip key={skill} label={skill} color="primary" variant="outlined" />
                    ))}
                </Stack>
            </Container>

            <Box
                sx={{
                    py: 10,
                    background:
                        "linear-gradient(180deg,#ffffff,#f8fafc)",
                }}
            >
                <Container maxWidth="lg">
                    <Typography
                        variant="h3"
                        align="center"
                        fontWeight={700}
                        gutterBottom
                    >
                        Services
                    </Typography>

                    <Grid container spacing={4} mt={2}>
                        {services.map((service) => (
                            <Grid key={service.title} size={{ xs: 12, md: 4 }}>
                                <Card
                                    sx={{
                                        height: "100%",
                                        transition: ".3s",
                                        "&:hover": {
                                            transform: "translateY(-8px)",
                                            boxShadow: 8,
                                        },
                                    }}
                                >
                                    <CardContent sx={{ textAlign: "center", p: 4 }}>
                                        {service.icon}

                                        <Typography variant="h5" mt={2}>
                                            {service.title}
                                        </Typography>

                                        <Typography color="text.secondary" mt={2}>
                                            {service.desc}
                                        </Typography>
                                    </CardContent>
                                </Card>
                            </Grid>
                        ))}
                    </Grid>
                </Container>
            </Box>

            <Container maxWidth="lg" sx={{ py: 10 }}>
                <Typography variant="h3" align="center" fontWeight={700} gutterBottom>
                    Featured Projects
                </Typography>

                <Grid container spacing={4} mt={2}>
                    {projects.map((project) => (
                        <Grid key={project.title} size={{ xs: 12, md: 4 }}>
                            <Card
                                sx={{
                                    height: "100%",
                                    transition: ".3s",
                                    "&:hover": {
                                        transform: "translateY(-8px)",
                                        boxShadow: 8,
                                    },
                                }}
                            >
                                <CardContent>
                                    {project.icon}

                                    <Typography variant="h5" mt={2}>
                                        {project.title}
                                    </Typography>

                                    <Typography color="text.secondary" mt={2}>
                                        {project.desc}
                                    </Typography>
                                </CardContent>
                            </Card>
                        </Grid>
                    ))}
                </Grid>
            </Container>

            <Box
                sx={{
                    py: 10,
                    background:
                        "linear-gradient(135deg,#2563EB,#7C3AED)",
                    color: "#fff",
                }}
            >
                <Container maxWidth="md">
                    <Typography variant="h3" align="center" fontWeight={700}>
                        Let's Build Something Amazing
                    </Typography>

                    <Typography align="center" sx={{ mt: 2, opacity: .9 }}>
                        Ready to turn your idea into a modern software solution?
                    </Typography>

                    <Stack
                        direction="row"
                        spacing={2}
                        justifyContent="center"
                        mt={5}
                    >
                        <Button
                            component={Link}
                            to="/register"
                            variant="contained"
                            color="inherit"
                        >
                            Book Appointment
                        </Button>

                        <Button
                            component={Link}
                            to="/login"
                            variant="outlined"
                            color="inherit"
                        >
                            Login
                        </Button>
                    </Stack>
                </Container>
            </Box>
        </>
    );
}
