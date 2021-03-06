﻿#region License

// Copyright (c) 2007-2009, Sean Chambers <schambers80@gmail.com>
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System;
using FluentMigrator.Infrastructure;
using NUnit.Framework;
using NUnit.Should;

namespace FluentMigrator.Tests.Unit {
    [TestFixture]
    public class GateTests {
        private Gate gate;

        [SetUp]
        public void Setup() {
            gate = new Gate();
        }

        [Test]
        public void GateShouldBeOpenedWhenStartAndEndDateIsToday() {
            gate.SetGate(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1));

            gate.IsOpen.ShouldBeTrue();
        }

        [Test]
        public void GateShouldBeOpenedWhenToHaveOnlyStartDateBeforeToday() {
            gate.SetGate(DateTime.Now.AddDays(-1), null);

            gate.IsOpen.ShouldBeTrue();
        }

        [Test]
        public void GateShouldBeOpenedWhenToHaveOnlyEndDateAfterToday() {
            gate.SetGate(null, DateTime.Now.AddDays(1));

            gate.IsOpen.ShouldBeTrue();
        }

        [Test]
        public void GateShouldBeNoOpenedWhenNotInformed() {
            gate.IsOpen.ShouldBeFalse();
        }

        [Test]
        public void GateShouldBeNoOpenedWhenTheDateIsPassed() {
            gate.SetGate(DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));

            gate.IsOpen.ShouldBeFalse();
        }

        [Test]
        public void GateShouldBeNoOpenedWhenToHaveStartDateThatNotArriveYet() {
            gate.SetGate(DateTime.Now.AddDays(1), null);

            gate.IsOpen.ShouldBeFalse();
        }

        [Test]
        public void GateShouldBeNoOpenedWhenToHavEndDatePassed() {
            gate.SetGate(null, DateTime.Now.AddDays(-1));

            gate.IsOpen.ShouldBeFalse();
        }

        [Test]
        public void GateToStringShouldShowTheStartAndTheEnd() {
            var start = DateTime.Now.AddDays(-1);
            var end = DateTime.Now.AddDays(1);

            gate.SetGate(start, end);

            Assert.That(gate.ToString(), Is.EqualTo(string.Format("Gate start in {0} until {1}.", start, end)));
        }

        [Test]
        public void GateToStringShouldShowTheStart() {
            var start = DateTime.Now.AddDays(-1);
            gate.SetGate(start, null);

            Assert.That(gate.ToString(), Is.EqualTo(string.Format("Gate start in {0}.", start)));
        }

        [Test]
        public void GateToStringShouldShowAndTheEnd() {
            var end = DateTime.Now.AddDays(1);
            gate.SetGate(null, end);

            Assert.That(gate.ToString(), Is.EqualTo(string.Format("Gate ending until {0}.", end)));
        }

        [Test]
        public void GateToStringShouldShowEmptyWhenNotInformed() {
            Assert.That(gate.ToString(), Is.Empty);
        }

        [Test]
        public void GateShouldBeNotEmptyWhenStartAndEndDateIsFilled() {
            gate.SetGate(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1));

            gate.IsEmpty().ShouldBeFalse();
        }

        [Test]
        public void GateShouldBeNotEmptyWhenToHaveOnlyStart() {
            gate.SetGate(DateTime.Now.AddDays(-1), null);

            gate.IsEmpty().ShouldBeFalse();
        }

        [Test]
        public void GateShouldBeNotEmptyWhenToHaveOnlyEndDate() {
            gate.SetGate(null, DateTime.Now.AddDays(1));

            gate.IsEmpty().ShouldBeFalse();
        }

        [Test]
        public void GateShouldBeEmptyWhenNotInformedStartOrEnd() {
            gate.SetGate(null, null);

            gate.IsEmpty().ShouldBeTrue();
        }
    }
}