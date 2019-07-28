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
WORKDIR /d0s_orch/
ENTRYPOINT [ "dotnet", "run", "--project", "Lists.Processor" ]

