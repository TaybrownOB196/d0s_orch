FROM centos:7

RUN yum update -y
RUN yum install -y lttng-ust libcurl openssl-libs krb5-libs libicu zlib wget

RUN wget https://dot.net/v1/dotnet-install.sh
RUN sh dotnet-install.sh -c 2.2 -arch x64 --install-dir ~/cli
ENV PATH=/root/cli:$PATH

RUN yum install git -y
RUN git clone https://github.com/TaybrownOB196/d0s_orch.git
RUN cd d0s_orch && \
    dotnet build Lists.Processor



FROM centos:7

RUN yum update -y
RUN yum install -y lttng-ust libcurl openssl-libs krb5-libs libicu zlib wget

RUN wget https://dot.net/v1/dotnet-install.sh
RUN sh dotnet-install.sh -c 2.2 -arch x64 --runtime dotnet --install-dir ~/cli
ENV PATH=/root/cli:$PATH

RUN mkdir services/
WORKDIR /services/
COPY --from=0 d0s_orch/Lists.Processor/bin/Debug/netcoreapp2.2 .

ENTRYPOINT [ "dotnet", "--project", "Lists.Processor" ]

